using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BasicAuthentication.Models;
using Microsoft.AspNetCore.Http;

namespace BasicAuthentication.Middlewares
{
    public class BasihAuthMiddlewares{
        private readonly RequestDelegate _next;

        public BasihAuthMiddlewares(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext httpContext){
            string authHeader = httpContext.Request.Headers["Authorization"];

            if(authHeader != null && authHeader.StartsWith("Basic")){

                string encodeUsernameAndPassword = authHeader.Substring("Basic".Length).Trim();
                Encoding encoding = Encoding.GetEncoding("UTF-8");
                string usernameAndPassword = encoding.GetString(Convert.FromBase64String(encodeUsernameAndPassword)); 
                int index = usernameAndPassword.IndexOf(":");
                var username = usernameAndPassword.Substring(0,index);
                var password = usernameAndPassword.Substring(index+1);

                if(username.Equals("abc") && password.Equals("123")){
                    await _next.Invoke(httpContext);
                }else{
                    httpContext.Response.StatusCode = 401;
                    await httpContext.Response.WriteAsync(authErrorResponse());
                    return;
                }

            } else{
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsync(authErrorResponse());
                return;
            }
        }

        private string authErrorResponse(){
            var model = new ErrorModel(){
                Error="Authentication Error",
                Code = "401",
            };
            
            return JsonSerializer.Serialize(model);
        }
    }
}
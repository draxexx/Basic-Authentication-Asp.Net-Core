using System.Collections.Generic;
using BasicAuthentication.Models;
using Microsoft.AspNetCore.Mvc;

namespace  BasicAuthentication.Controllers
{
    [Route("api")]
    public class DemoController : Controller{
        
        [Route("demo1")]
        public IActionResult Demo1(){
            try{
                var model = new List<ProductModel>(){
                    new ProductModel(){
                        Id=1,
                        Name="Computer",
                        Price=7999.99,
                    },
                    new ProductModel(){
                        Id=2,
                        Name="Phone",
                        Price=2975.99,
                    },
                    new ProductModel(){
                        Id=3,
                        Name="Tablet",
                        Price=3455,
                    },
                };
                return Ok(model);
            }catch{
                return BadRequest();
            }
        }

        [Produces("text/plain")]
        [Route("demo2")]
        public IActionResult Demo2(){
            try{
                return Ok("Demo 2");
            }catch{
                return BadRequest();
            }
        }

    }
}
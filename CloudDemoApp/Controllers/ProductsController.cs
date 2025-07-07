using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloudDemoApp.Controllers
{
    [ApiController]
    [Route("Products")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        [Route("ListProducts")]
        public List<string> GetAll()
        {
            var products = new List<string>
            {
                "SODA",
                "MILK"
            };
            return products;
        }
    }
}

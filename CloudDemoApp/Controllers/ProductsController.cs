using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloudDemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet("ListProduct")]
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

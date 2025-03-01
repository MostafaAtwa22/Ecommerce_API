using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ProductsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Product>>> GetAll()
        {
            var products = await context.Products.ToListAsync();
            return Ok(products);
        }

        [HttpGet("GetById/{id:int}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await context.Products.FindAsync(id);

            if (product is null)
                return NotFound($"Product With ID : {id} is not exists");

            return Ok(product);
        }

        [HttpPost("Create")]
        public IActionResult Create()
        {
            return Ok();
        }

        [HttpPut("Update")]
        public IActionResult Update()
        {
            return Ok();
        }

        [HttpDelete("Delete")]
        public IActionResult Delete()
        {
            return Ok();
        }
    }
}

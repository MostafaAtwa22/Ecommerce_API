namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetAll()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("GetById/{id:int}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

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

        [HttpDelete("Delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetByIdAsync(id);

            if (product is null)
                return NotFound($"The Product with Id : {id} is not exists");

            return Ok(product);
        }
    }
}

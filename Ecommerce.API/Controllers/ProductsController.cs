using AutoMapper;
using Ecommerce.API.Dtos;
using Ecommerce.Core.Specifications;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IReadOnlyList<ProductDetailsDto>>> GetAll()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products = await _productRepository.GetAllWithSpec(spec);


            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDetailsDto>>(products));
        }

        [HttpGet("GetById/{id:int}")]
        public async Task<ActionResult<ProductDetailsDto>> GetById(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productRepository.GetWithSpec(spec);

            if (product is null)
                return NotFound($"Product With ID : {id} is not exists");

            return Ok(_mapper.Map<Product, ProductDetailsDto>(product));
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

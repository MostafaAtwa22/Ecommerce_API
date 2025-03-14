using AutoMapper;
using Ecommerce.API.Dtos;
using Ecommerce.API.Errors;
using Ecommerce.Core.Specifications;

namespace Ecommerce.API.Controllers
{
    public class ProductsController : BaseAPIController
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet("GetAll/{sort:alpha}")]
        public async Task<ActionResult<IReadOnlyList<ProductDetailsDto>>> GetAll(string sort)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(sort);
            var products = await _productRepository.GetAllWithSpec(spec);


            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDetailsDto>>(products));
        }

        [HttpGet("GetById/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDetailsDto>> GetById(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productRepository.GetWithSpec(spec);

            if (product is null)
                return NotFound(new ApiResponse(404));

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

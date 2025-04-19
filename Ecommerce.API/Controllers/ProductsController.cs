using AutoMapper;
using Ecommerce.API.Dtos;
using Ecommerce.API.Errors;
using Ecommerce.Core.Specifications;

namespace Ecommerce.API.Controllers
{
    public class ProductsController : BaseAPIController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IReadOnlyList<ProductDetailsDto>>> GetAll(
            string? sort,int? brandId, int? typeId)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(sort, brandId, typeId);
            var products = await _unitOfWork.Repository<Product>().GetAllWithSpec(spec);


            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDetailsDto>>(products));
        }

        [HttpGet("GetById/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDetailsDto>> GetById(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _unitOfWork.Repository<Product>().GetWithSpec(spec);

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
        public async Task<ActionResult<ProductDetailsDto>> Delete(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _unitOfWork.Repository<Product>().GetWithSpec(spec);

            if (product == null)
                return NotFound($"The Product with Id : {id} does not exist");

            _unitOfWork.Repository<Product>().Delete(product);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
                return BadRequest(new ApiResponse(400, "Couldn't delete this product"));

            var productDto = _mapper.Map<ProductDetailsDto>(product);
            return Ok(productDto);
        }

    }
}

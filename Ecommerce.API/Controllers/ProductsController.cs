using AutoMapper;
using Ecommerce.API.Dtos;
using Ecommerce.API.Errors;
using Ecommerce.API.Helpers;
using Ecommerce.Core.Specifications;
using Ecommerce.Infrastructure.FileSettings;

namespace Ecommerce.API.Controllers
{
    public class ProductsController : BaseAPIController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImageService _imageService;

        public ProductsController(IUnitOfWork unitOfWork,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _imageService = imageService;
        }

        [Cached(600)]
        [HttpGet("GetAll")]
        public async Task<ActionResult<IReadOnlyList<ProductDetailsDto>>> GetAll(
            string? sort,int? brandId, int? typeId)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(sort, brandId, typeId);
            var products = await _unitOfWork.Repository<Product>().GetAllWithSpec(spec);


            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDetailsDto>>(products));
        }

        [Cached(600)]
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
        public async Task<ActionResult<ProductDetailsDto>> Create([FromForm] CreateProductDto dto)
        {
            var pictureName = await _imageService.SaveImageAsync(dto.PictureUrl);

            var product = _mapper.Map<Product>(dto);
            product.PictureUrl = pictureName;

            _unitOfWork.Repository<Product>().Add(product);
            await _unitOfWork.Complete();

            var productDetailsDto = _mapper.Map<ProductDetailsDto>(product);
            return Ok(productDetailsDto);
        }

        [HttpPut("Update/{id:int}")]
        public async Task<ActionResult<ProductDetailsDto>> Update(int id, [FromForm] UpdateProductDto dto)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(id);
            if (product is null)
                return NotFound(new ApiResponse(404, $"Product with id {id} not found."));

            if (dto.PictureUrl is not null)
            {
                _imageService.DeleteImage(product.PictureUrl);
                var newImageName = await _imageService.SaveImageAsync(dto.PictureUrl);
                product.PictureUrl = newImageName;
            }

            _mapper.Map(dto, product);

            var result = await _unitOfWork.Complete();
            if (result <= 0)
                return BadRequest(new ApiResponse(400, "Problem updating product."));

            var updatedDto = _mapper.Map<ProductDetailsDto>(product);
            return Ok(updatedDto);
        }

        [HttpDelete("Delete/{id:int}")]
        public async Task<ActionResult<ProductDetailsDto>> Delete(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _unitOfWork.Repository<Product>().GetWithSpec(spec);

            if (product == null)
                return NotFound($"The Product with Id : {id} does not exist");

            if (!string.IsNullOrEmpty(product.PictureUrl))
                _imageService.DeleteImage(product.PictureUrl);

            _unitOfWork.Repository<Product>().Delete(product);
            var result = await _unitOfWork.Complete();

            if (result <= 0)
                return BadRequest(new ApiResponse(400, "Couldn't delete this product"));

            var productDto = _mapper.Map<ProductDetailsDto>(product);
            return Ok(productDto);
        }

    }
}

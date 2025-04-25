using Ecommerce.API.Dtos;
using AutoMapper;
using Ecommerce.API.Errors;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.API.Controllers
{
    public class ProductBrandsController : BaseAPIController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductBrandsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ProductBrandDto>>> GetAll()
        {
            var productBrands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
            var productBrandDtos = _mapper.Map<IEnumerable<ProductBrandDto>>(productBrands);
            return Ok(productBrandDtos);
        }

        [HttpGet("GetById/{id:int}")]
        public async Task<ActionResult<ProductBrandDto>> GetById(int id)
        {
            var productBrand = await _unitOfWork.Repository<ProductBrand>().GetByIdAsync(id);
            if (productBrand == null)
                return NotFound(new ApiResponse(404, $"ProductBrand with id {id} not found."));

            var productBrandDto = _mapper.Map<ProductBrandDto>(productBrand);
            return Ok(productBrandDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public async Task<ActionResult<ProductBrandDto>> Create(ProductBrandDto dto)
        {
            var productBrand = _mapper.Map<ProductBrand>(dto);
            _unitOfWork.Repository<ProductBrand>().Add(productBrand);
            var result = await _unitOfWork.Complete();
            if (result <= 0)
                return BadRequest(new ApiResponse(400, "Problem Create product brand."));

            return Ok(dto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Update/{id:int}")]
        public async Task<ActionResult<ProductBrandDto>> Update(int id, ProductBrandDto dto)
        {
            var productBrand = await _unitOfWork.Repository<ProductBrand>().GetByIdAsync(id);
            if (productBrand == null)
                return NotFound(new ApiResponse(404, $"ProductBrand with id {id} not found."));

            _mapper.Map(dto, productBrand);
            var result = await _unitOfWork.Complete();
            if (result <= 0)
                return BadRequest(new ApiResponse(400, "Problem updating product brand."));

            var updatedDto = _mapper.Map<ProductBrandDto>(productBrand);
            return Ok(updatedDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id:int}")]
        public async Task<ActionResult<ProductBrandDto>> Delete(int id)
        {
            var productBrand = await _unitOfWork.Repository<ProductBrand>().GetByIdAsync(id);
            if (productBrand == null)
                return NotFound(new ApiResponse(404, $"ProductBrand with id {id} not found."));

            _unitOfWork.Repository<ProductBrand>().Delete(productBrand);
            var result = await _unitOfWork.Complete();
            if (result <= 0)
                return BadRequest(new ApiResponse(400, "Problem deleting product brand."));

            var productBrandDto = _mapper.Map<ProductBrandDto>(productBrand);
            return Ok(productBrandDto);
        }
    }
}

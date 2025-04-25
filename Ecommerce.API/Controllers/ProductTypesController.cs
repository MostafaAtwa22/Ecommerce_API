using Ecommerce.API.Dtos;
using AutoMapper;
using Ecommerce.API.Errors;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.API.Controllers
{
    public class ProductTypesController : BaseAPIController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductTypesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ProductTypeDto>>> GetAll()
        {
            var productTypes = await _unitOfWork.Repository<ProductType>().GetAllAsync();
            var productTypeDtos = _mapper.Map<IEnumerable<ProductTypeDto>>(productTypes);
            return Ok(productTypeDtos);
        }

        [HttpGet("GetById/{id:int}")]
        public async Task<ActionResult<ProductTypeDto>> GetById(int id)
        {
            var productType = await _unitOfWork.Repository<ProductType>().GetByIdAsync(id);
            if (productType == null)
                return NotFound(new ApiResponse(404, $"ProductType with id {id} not found."));

            var productTypeDto = _mapper.Map<ProductTypeDto>(productType);
            return Ok(productTypeDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public async Task<ActionResult<ProductTypeDto>> Create(ProductTypeDto dto)
        {
            var productType = _mapper.Map<ProductType>(dto);
            _unitOfWork.Repository<ProductType>().Add(productType);
            var result = await _unitOfWork.Complete();
            if (result <= 0)
                return BadRequest(new ApiResponse(400, "Problem Create product type."));

            return Ok(dto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Update/{id:int}")]
        public async Task<ActionResult<ProductTypeDto>> Update(int id, ProductTypeDto dto)
        {
            var productType = await _unitOfWork.Repository<ProductType>().GetByIdAsync(id);
            if (productType == null)
                return NotFound(new ApiResponse(404, $"ProductType with id {id} not found."));

            _mapper.Map(dto, productType);
            var result = await _unitOfWork.Complete();
            if (result <= 0)
                return BadRequest(new ApiResponse(400, "Problem updating product type."));

            var updatedDto = _mapper.Map<ProductTypeDto>(productType);
            return Ok(updatedDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id:int}")]
        public async Task<ActionResult<ProductTypeDto>> Delete(int id)
        {
            var productType = await _unitOfWork.Repository<ProductType>().GetByIdAsync(id);
            if (productType == null)
                return NotFound(new ApiResponse(404, $"ProductType with id {id} not found."));

            _unitOfWork.Repository<ProductType>().Delete(productType);
            var result = await _unitOfWork.Complete();
            if (result <= 0)
                return BadRequest(new ApiResponse(400, "Problem deleting product type."));

            var productTypeDto = _mapper.Map<ProductTypeDto>(productType);
            return Ok(productTypeDto);
        }
    }
}

using AutoMapper;
using Ecommerce.API.Dtos;
using Ecommerce.API.Errors;

namespace Ecommerce.API.Controllers
{
    public class BasketController : BaseAPIController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<CustomerBasketDto>> GetById(string id)
        {
            var basket = await _basketRepository.GetAsync(id);

            if (basket is null)
                return Ok(new CustomerBasketDto { Id = id });

            var basketDto = _mapper.Map<CustomerBasketDto>(basket);
            return Ok(basketDto);
        }

        [HttpPost("Update")]
        public async Task<ActionResult<CustomerBasketDto>> Update(CustomerBasketDto basketDto)
        {
            var customerBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basketDto);

            var updatedBasket = await _basketRepository.UpdateAsync(customerBasket);

            var updatedBasketDto = _mapper.Map<CustomerBasketDto>(updatedBasket);

            return Ok(updatedBasketDto);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<CustomerBasket>> Delete(string id)
        {
            var basket = await _basketRepository.DeleteAsync(id);

            if (!basket)
                return NotFound(new ApiResponse(404));

            return Ok("The data deleted !!");
        }
    }
}

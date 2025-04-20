using AutoMapper;
using Ecommerce.API.Dtos;
using Ecommerce.API.Errors;
using Ecommerce.API.Extensions;
using Ecommerce.Core.Models.OrderAggregate;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Ecommerce.API.Controllers
{
    [Authorize]
    public class OrdersController : BaseAPIController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Order>> Create(OrderDto dto)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var address = _mapper.Map<UserAddressDto, Address>(dto.ShipToAddress);

            var order = await _orderService.CreateOrderAsync(email, dto.DeliveredMethod, dto.BasketId, address);

            if (order is null)
                return BadRequest(new ApiResponse(400, "Problem Create order"));

            return Ok(order);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var orders = await _orderService.GetOrderAsync(email);

            return Ok(_mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
        }

        [HttpGet("GetById{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var order = await _orderService.GetOrderByIdAsync(id, email);

            if (order is null)
                return NotFound(new ApiResponse(404, $"The order with Id = {id} is not found"));

            return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
        }

        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
            => Ok(await _orderService.GetDeliveryMethodAsync());
    }
}

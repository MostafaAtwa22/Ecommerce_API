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
    }
}

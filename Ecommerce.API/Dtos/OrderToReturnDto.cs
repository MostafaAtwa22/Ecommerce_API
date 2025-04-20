using Ecommerce.Core.Models.OrderAggregate;

namespace Ecommerce.API.Dtos
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }

        public string BuyerEmail { get; set; } = string.Empty;

        public DateTimeOffset OrderDate { get; set; }

        public Address ShipToAddress { get; set; } = default!;

        public string DeliveryMethod { get; set; } = string.Empty;

        public decimal ShippingPrice { get; set; }

        public IReadOnlyList<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();

        public decimal SubTotal { get; set; }

        public decimal Total { get; set; }

        public string Status { get; set; } = string.Empty;

    }
}

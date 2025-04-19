namespace Ecommerce.Core.Models.OrderAggregate
{
    public class Order : BaseEntity
    {
        public Order()
        {
            
        }
        public Order(string buyerEmail, Address shipToAddress, DeliveryMethod deliveryMethod, 
            IReadOnlyList<OrderItem> orderItems, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            SubTotal = subTotal;
        }

        public string BuyerEmail { get; set; } = string.Empty;

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public Address ShipToAddress { get; set; } = default!;

        public DeliveryMethod DeliveryMethod { get; set; } = default!;

        public IReadOnlyList<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public decimal SubTotal { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public string PaymentIntentId { get; set; } = string.Empty;

        public decimal GetTotal()
            => SubTotal + DeliveryMethod.Price;
    }
}
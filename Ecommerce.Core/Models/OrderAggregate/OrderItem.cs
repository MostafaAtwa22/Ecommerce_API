namespace Ecommerce.Core.Models.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {
        }

        public OrderItem(ProductItemOrdered itemOrdered, decimal price, int quantity)
        {
            ItemOrdered = itemOrdered;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrdered ItemOrdered { get; set; } = default!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}

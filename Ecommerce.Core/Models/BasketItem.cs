namespace Ecommerce.Core.Models
{
    public class BasketItem 
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;

        public string CustomerBasketId { get; set; } = string.Empty;
        public virtual CustomerBasket CustomerBasket { get; set; } = default!;
    }
}

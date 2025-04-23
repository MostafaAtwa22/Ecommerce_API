namespace Ecommerce.API.Dtos
{
    public class CustomerBasketDto
    {
        public string Id { get; set; } = string.Empty;

        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();

        public int? DeliveryMethodId { get; set; }

        public string ClientSecret { get; set; } = string.Empty;

        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
 
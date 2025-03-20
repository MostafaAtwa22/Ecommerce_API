namespace Ecommerce.API.Dtos
{
    public class CustomerBasketDto
    {
        public string Id { get; set; } = string.Empty;
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
    }
}
 
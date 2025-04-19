using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.Dtos
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }

        [MinLength(3), MaxLength(255)]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [Range(0.1, double.MaxValue,
            ErrorMessage = "Price must be more than Zero")]
        public decimal Price { get; set; }

        [Range(1, 10,
            ErrorMessage = "Quantity must be more than Zero")]
        public int Quantity { get; set; }

        public string PictureUrl { get; set; } = string.Empty;

        [MinLength(3), MaxLength(255)]
        public string Brand { get; set; } = string.Empty;

        [MinLength(3), MaxLength(255)]
        public string Type { get; set; } = string.Empty;
    }
    public class OrderDto
    {
        public string BasketId { get; set; } = string.Empty;
        public int DeliveredMethod { get; set; }

        public UserAddressDto ShipToAddress { get; set; } = default!;
    }
}
 
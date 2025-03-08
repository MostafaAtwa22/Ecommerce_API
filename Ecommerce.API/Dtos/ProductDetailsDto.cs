using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.Dtos
{
    public class ProductDetailsDto
    {
        public int Id { get; set; }

        [MinLength(3), MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string PictureUrl { get; set; } = string.Empty;

        public string ProductType { get; set; } = default!;

        public string ProductBrand { get; set; } = default!;
    }
}

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.Dtos
{
    public class ProductDto
    {
        [MinLength(3), MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int ProductTypeId { get; set; }

        public int ProductBrandId { get; set; }
    }
}

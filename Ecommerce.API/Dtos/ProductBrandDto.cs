using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.Dtos
{
    public class ProductBrandDto
    {
        [MinLength(3), MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}

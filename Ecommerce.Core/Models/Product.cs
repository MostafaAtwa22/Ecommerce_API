namespace Ecommerce.Core.Models
{
    public class Product : BaseEntity
    {
        [Required]
        [MinLength(3), MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;
    }
}

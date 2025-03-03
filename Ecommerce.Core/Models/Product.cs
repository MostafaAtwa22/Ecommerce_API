namespace Ecommerce.Core.Models
{
    public class Product : BaseEntity
    {
        [MinLength(3), MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string PictureUrl { get; set; } = string.Empty;

        public int ProductTypeId { get; set; }

        public virtual ProductType ProductType { get; set; } = default!;

        public int ProductBrandId { get; set; }

        public virtual ProductBrand ProductBrand { get; set; } = default!;
    }
}

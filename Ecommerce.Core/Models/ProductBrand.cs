namespace Ecommerce.Core.Models
{
    public class ProductBrand : BaseEntity
    {
        [MinLength(2), MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}

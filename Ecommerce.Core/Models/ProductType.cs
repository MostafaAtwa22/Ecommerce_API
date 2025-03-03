namespace Ecommerce.Core.Models
{
    public class ProductType : BaseEntity
    {
        [MinLength(3), MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}

namespace Ecommerce.Core.Models.Caching
{
    public class CacheEntry
    {
        [Key]
        public string CacheKey { get; set; } = string.Empty;
        public string SerializedResponse { get; set; } = string.Empty;
        public DateTime ExpiryTime { get; set; }
    }

}

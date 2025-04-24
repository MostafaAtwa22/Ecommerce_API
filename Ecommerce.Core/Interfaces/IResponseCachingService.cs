namespace Ecommerce.Core.Interfaces
{
    public interface IResponseCachingService
    {
        Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive);
        Task<string?> GetCachedResponseAsync(string cacheKey);
    }
}

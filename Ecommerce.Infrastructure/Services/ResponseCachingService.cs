using Ecommerce.Core.Models.Caching;
using System.Text.Json;

namespace Ecommerce.Infrastructure.Services
{
    public class ResponseCachingService : IResponseCachingService
    {
        private readonly ApplicationDbContext _context;

        public ResponseCachingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
        {
            if (response is null)
                return;

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var serializedResponse = JsonSerializer.Serialize(response, options);

            var expiry = DateTime.UtcNow.Add(timeToLive);

            var existingEntry = await _context.CacheEntries.FindAsync(cacheKey);

            if (existingEntry is not null)
            {
                existingEntry.SerializedResponse = serializedResponse;
                existingEntry.ExpiryTime = expiry;
                _context.CacheEntries.Update(existingEntry);
            }
            else
            {
                var cacheEntry = new CacheEntry
                {
                    CacheKey = cacheKey,
                    SerializedResponse = serializedResponse,
                    ExpiryTime = expiry
                };
                await _context.CacheEntries.AddAsync(cacheEntry);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<string?> GetCachedResponseAsync(string cacheKey)
        {
            var entry = await _context.CacheEntries.FindAsync(cacheKey);

            if (entry == null || entry.ExpiryTime < DateTime.UtcNow)
                return null!;

            return entry.SerializedResponse;
        }
    }
}
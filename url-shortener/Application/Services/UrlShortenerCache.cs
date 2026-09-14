using Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Services
{
    public class UrlShortenerCache(IDistributedCache distributedCache) : IUrlShortenerCache
    {
        public Task<string?> GetOriginalUrlAsync(string shortCode, CancellationToken cancellationToken = default)
        {
            return distributedCache.GetStringAsync(shortCode, cancellationToken);
        }

        public Task SetOriginalUrlAsync(string shortCode, string originalUrl, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
        {
            return distributedCache.SetStringAsync(shortCode, originalUrl, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromHours(24)
            }, cancellationToken);
        }
    }
}

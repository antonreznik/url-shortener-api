using Application.Interfaces;

namespace Application.UrlShortener.GetOriginal
{
    public class GetOriginalUrlRequestHandler(
        IUrlShortenerRepository urlShortenerRepository, 
        IUrlShortenerCache urlShortenerCache) : IGetOriginalUrlRequestHandler
    {
        public async Task<string> HandleAsync(GetOriginalUrlDto dto)
        {
            var cachedUrl = await urlShortenerCache.GetOriginalUrlAsync(dto.ShortCode);

            if (!string.IsNullOrEmpty(cachedUrl))
            {
                return cachedUrl;
            }

            var result = await urlShortenerRepository.GetByShortCodeAsync(dto.ShortCode);
            var originalUrl = result?.OriginalUrl;

            if (string.IsNullOrEmpty(originalUrl))
            {
                return string.Empty;
            }

            await urlShortenerCache.SetOriginalUrlAsync(dto.ShortCode, originalUrl);

            return originalUrl;
        }
    }
}

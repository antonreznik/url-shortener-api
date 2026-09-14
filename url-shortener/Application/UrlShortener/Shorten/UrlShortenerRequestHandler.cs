using Application.Interfaces;
using Domain;

namespace Application.UrlShortener.Shorten
{
    public class UrlShortenerRequestHandler(
        IUrlShortenerRepository urlShortenerRepository, 
        IUrlShortenerCache urlShortenerCache) : IUrlShortenerRequestHandler
    {
        public async Task<string> HandleAsync(UrlShortenerDto dto)
        {
            var uniqueId = await urlShortenerRepository.GetUrlIdSequenceAsync();

            var model = UrlShortenerModel.Create(dto.OriginalUrl, uniqueId);

            var persistedModel = await urlShortenerRepository.AddAsync(model);

            await urlShortenerCache.SetOriginalUrlAsync(persistedModel.ShortCode, persistedModel.OriginalUrl);

            return persistedModel.ShortCode;
        }
    }
}

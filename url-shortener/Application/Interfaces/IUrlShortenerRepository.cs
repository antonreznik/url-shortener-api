

using Domain;

namespace Application.Interfaces
{
    public interface IUrlShortenerRepository
    {
        Task<UrlShortenerModel> AddAsync(UrlShortenerModel model, CancellationToken cancellationToken = default);
        Task<UrlShortenerModel?> GetByShortCodeAsync(string shortCode, CancellationToken cancellationToken = default);
        Task<UrlShortenerModel?> GetByOriginalUrlAsync(string originalUrl, CancellationToken cancellationToken = default);
        Task<long> GetUrlIdSequenceAsync(CancellationToken cancellationToken = default);
    }
}

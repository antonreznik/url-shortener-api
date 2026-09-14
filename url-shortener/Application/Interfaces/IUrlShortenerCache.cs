namespace Application.Interfaces
{
    public interface IUrlShortenerCache
    {
        Task<string?> GetOriginalUrlAsync(string shortCode, CancellationToken cancellationToken = default);
        Task SetOriginalUrlAsync(string shortCode, string originalUrl, TimeSpan? expiration = null, CancellationToken cancellationToken = default);
    }
}

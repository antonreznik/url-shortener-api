
namespace Application.UrlShortener.GetOriginal
{
    public interface IGetOriginalUrlRequestHandler
    {
        Task<string> HandleAsync(GetOriginalUrlDto dto);
    }
}

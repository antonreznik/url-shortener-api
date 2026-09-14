namespace Application.UrlShortener.Shorten
{
    public interface IUrlShortenerRequestHandler
    {
        public Task<string> HandleAsync(UrlShortenerDto dto);
    }
}

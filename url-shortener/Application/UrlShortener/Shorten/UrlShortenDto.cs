using System.ComponentModel.DataAnnotations;

namespace Application.UrlShortener.Shorten
{
    public record UrlShortenerDto([property: Url] string OriginalUrl);
}

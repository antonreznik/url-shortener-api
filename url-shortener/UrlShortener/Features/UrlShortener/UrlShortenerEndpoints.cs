using Application.UrlShortener.Shorten;
using Application.UrlShortener.GetOriginal;

namespace UrlShortener.Features.UrlShortener
{
    public static class UrlShortenerEndpoints
    {
        public static void MapUrlShortenerEndpoints(this WebApplication app)
        {
            app.MapPost("/shorten", async (IUrlShortenerRequestHandler urlShortenerRequestHandler, UrlShortenerDto dto, HttpContext httpContext) =>
            {
                var validationErrors = UrlShortenDtoValidator.Validate(dto);

                if (validationErrors.Length > 0)
                {
                    return Results.ValidationProblem(new Dictionary<string, string[]>
                    {
                        ["Errors"] = validationErrors
                    });
                }

                var shortCode = await urlShortenerRequestHandler.HandleAsync(dto);

                var shortUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/{shortCode}";

                return Results.Ok(new { ShortUrl = shortUrl });
            });

            app.MapGet("/{shortCode}", async (IGetOriginalUrlRequestHandler getOriginalUrlRequestHandler, string shortCode) =>
            {
                var dto = new GetOriginalUrlDto(shortCode);

                var originalUrl = await getOriginalUrlRequestHandler.HandleAsync(dto);

                if (string.IsNullOrEmpty(originalUrl))
                {
                    return Results.NotFound();
                }

                return Results.Redirect(originalUrl);
            });
        }
    }
}

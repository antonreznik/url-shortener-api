
using Application.Interfaces;
using Application.UrlShortener.Shorten;
using Application.UrlShortener.GetOriginal;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ServiceExtensions
    {
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUrlShortenerRequestHandler, UrlShortenerRequestHandler>();
            services.AddScoped<IGetOriginalUrlRequestHandler, GetOriginalUrlRequestHandler>();
        }
    }
}

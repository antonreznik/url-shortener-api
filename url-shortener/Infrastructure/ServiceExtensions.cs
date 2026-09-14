using Application.Interfaces;
using Infrastructure.Repository;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceExtensions
    {
        public static void RegisterInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IUrlShortenerRepository, UrlShortenerRepository>();
            services.AddScoped<IUrlShortenerCache, UrlShortenerCache>();
        }
    }
}

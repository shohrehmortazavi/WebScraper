using Microsoft.Extensions.DependencyInjection;
using WebScraper.Application.CurrencyRates.BackgroundServices;
using WebScraper.Application.CurrencyRates.Services;
using WebScraper.DataAccess.SeedWorks;
using WebScraper.Domain.CurrencyRates.SeedWorks;

namespace WebScraper.Application.SeedWorks
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<CurrencyRateScraperService>();
            services.AddSingleton<CurrencyRateBackgroundService>();

            services.AddHostedService(
                provider => provider.GetRequiredService<CurrencyRateBackgroundService>());

            return services;
        }
    }
}

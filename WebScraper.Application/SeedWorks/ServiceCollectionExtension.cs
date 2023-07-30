using Microsoft.Extensions.DependencyInjection;
using WebScraper.Application.CurrencyRates.BackgroundServices;
using WebScraper.Application.CurrencyRates.Services;
using WebScraper.Application.MoneyRates.BackgroundServices;
using WebScraper.Application.MoneyRates.Services;
using WebScraper.DataAccess.SeedWorks;
using WebScraper.Domain.SeedWorks;

namespace WebScraper.Application.SeedWorks
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<CurrencyRateScraperService>();
            services.AddScoped<MoneyRateScraperService>();
            services.AddSingleton<CurrencyRateBackgroundService>();
            services.AddSingleton<MoneyRateBackgroundService>();

            services.AddHostedService(
                provider => provider.GetRequiredService<CurrencyRateBackgroundService>());

            services.AddHostedService(
                provider => provider.GetRequiredService<MoneyRateBackgroundService>());

            return services;
        }
    }
}

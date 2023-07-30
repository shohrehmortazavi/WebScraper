using Microsoft.Extensions.Configuration;
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
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<CurrencyRateScraperService>();
            services.AddScoped<MoneyRateScraperService>();
            services.AddSingleton<CurrencyRateBackgroundService>();
            services.AddSingleton<MoneyRateBackgroundService>();

            if (config != null)
            {
                var isCurrencyRateEnabled = Convert.ToBoolean(config.GetSection("BackgroundServicesSetting:CurrencyRateSetting:IsEnabled").Value);
                var isMoneyRateEnabled = Convert.ToBoolean(config.GetSection("BackgroundServicesSetting:MoneyRateSetting:IsEnabled").Value);

                if (isCurrencyRateEnabled)
                    services.AddHostedService(
                        provider => provider.GetRequiredService<CurrencyRateBackgroundService>());

                if (isMoneyRateEnabled)
                    services.AddHostedService(
                        provider => provider.GetRequiredService<MoneyRateBackgroundService>());
            }
            return services;
        }
    }
}

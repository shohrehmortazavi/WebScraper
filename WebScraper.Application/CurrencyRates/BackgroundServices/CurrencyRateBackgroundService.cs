using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebScraper.Application.CurrencyRates.Commands;
using WebScraper.Application.CurrencyRates.Services;
using WebScraper.Application.SeedWorks;

namespace WebScraper.Application.CurrencyRates.BackgroundServices
{
    public class CurrencyRateBackgroundService : BackgroundService
    {
        private readonly TimeSpan _period;
        private readonly IServiceScopeFactory _factory;
        private readonly ILogger<CurrencyRateBackgroundService> _logger;
        public readonly IOptions<BackgroundServicesSetting> _backgroundServiceSetting;
        private readonly CurrencyRateSetting _currencyRateSetting;
        private int _executionCount = 0;

        public CurrencyRateBackgroundService(ILogger<CurrencyRateBackgroundService> logger,
                                              IServiceScopeFactory factory,
                                              IOptions<BackgroundServicesSetting> backgroundServiceSetting)
        {
            _logger = logger;
            _factory = factory;
            _backgroundServiceSetting = backgroundServiceSetting;
            _currencyRateSetting = _backgroundServiceSetting.Value.CurrencyRate;
            _period = TimeSpan.FromSeconds(_currencyRateSetting.RepeatedTime);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new PeriodicTimer(_period);

            // When ASP.NET Core is intentionally shut down, the background service receives information
            // via the stopping token that it has been canceled.
            // We check the cancellation to avoid blocking the application shutdown.
            while (
                !stoppingToken.IsCancellationRequested &&
                await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    if (_currencyRateSetting.IsEnabled)
                    {
                        await using AsyncServiceScope asyncScope = _factory.CreateAsyncScope();

                        var _currencyRateScraperService = asyncScope.ServiceProvider.GetRequiredService<CurrencyRateScraperService>();
                        var result = _currencyRateScraperService.GetCurrencyRate();

                        var mediator = asyncScope.ServiceProvider.GetRequiredService<IMediator>();
                        var response = await mediator.Send(new CreateCurrencyRateCommand(result));
                        _logger.LogInformation("CurrencyRate Created!");

                        _executionCount++;
                        _logger.LogInformation(
                            $"Executed CurrencyRateBackgroundService - Count: {_executionCount}");
                    }
                    else
                    {
                        _logger.LogInformation(
                            "Skipped CurrencyRateBackgroundService");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(
                        $"Failed to execute CurrencyRateBackgroundService with exception message {ex.Message}. Good luck next round!");
                }
            }
        }
    }
}
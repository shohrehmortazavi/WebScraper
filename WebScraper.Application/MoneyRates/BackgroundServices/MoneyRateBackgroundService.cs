using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebScraper.Application.MoneyRates.Commands;
using WebScraper.Application.MoneyRates.Services;
using WebScraper.Application.SeedWorks;

namespace WebScraper.Application.MoneyRates.BackgroundServices
{
    public class MoneyRateBackgroundService : BackgroundService
    {
        private readonly TimeSpan _period;
        private readonly IServiceScopeFactory _factory;
        private readonly ILogger<MoneyRateBackgroundService> _logger;
        public readonly IOptions<BackgroundServicesSetting> _backgroundServiceSetting;
        private readonly MoneyRateSetting _moneyRateSetting;
        private int _executionCount = 0;

        public MoneyRateBackgroundService(ILogger<MoneyRateBackgroundService> logger,
                                          IServiceScopeFactory factory,
                                          IOptions<BackgroundServicesSetting> backgroundServiceSetting)
        {
            _logger = logger;
            _factory = factory;

            _backgroundServiceSetting = backgroundServiceSetting;
            _moneyRateSetting = _backgroundServiceSetting.Value.MoneyRateSetting;
            _period = TimeSpan.FromSeconds(_moneyRateSetting.RepeatedTime);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new PeriodicTimer(_period);

            while (
                !stoppingToken.IsCancellationRequested &&
                await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    await using AsyncServiceScope asyncScope = _factory.CreateAsyncScope();

                    var _moneyRateScraperService = asyncScope.ServiceProvider.GetRequiredService<MoneyRateScraperService>();
                    var moneyRates = _moneyRateScraperService.GetMoneyRates();

                    var mediator = asyncScope.ServiceProvider.GetRequiredService<IMediator>();
                    foreach (var moneyRate in moneyRates)
                    {
                        var response = await mediator.Send(new CreateMoneyRateCommand(moneyRate));
                        _logger.LogInformation("MoneyRate Created!");
                    }

                    _executionCount++;
                    _logger.LogInformation(
                        $"Executed MoneyRateBackgroundService - Count: {_executionCount}");
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(
                        $"Failed to execute MoneyRateBackgroundService with exception message {ex.Message}. Good luck next round!");
                }
            }
        }
    }
}
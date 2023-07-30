using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebScraper.Application.MoneyRates.Commands;
using WebScraper.Application.MoneyRates.Services;

namespace WebScraper.Application.MoneyRates.BackgroundServices
{
    public class MoneyRateBackgroundService : BackgroundService
    {
        private readonly TimeSpan _period = TimeSpan.FromSeconds(30);
        private readonly ILogger<MoneyRateBackgroundService> _logger;
        private readonly IServiceScopeFactory _factory;
        private int _executionCount = 0;
        public bool IsEnabled { get; set; }

        public MoneyRateBackgroundService(
            ILogger<MoneyRateBackgroundService> logger,
            IServiceScopeFactory factory)
        {
            _logger = logger;
            _factory = factory;
            IsEnabled = true;
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
                    if (IsEnabled)
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
                    else
                    {
                        _logger.LogInformation(
                            "Skipped MoneyRateBackgroundService");
                    }
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
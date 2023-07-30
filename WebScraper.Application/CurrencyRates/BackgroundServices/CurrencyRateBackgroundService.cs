﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebScraper.Application.CurrencyRates.Commands;
using WebScraper.Application.CurrencyRates.Services;

namespace WebScraper.Application.CurrencyRates.BackgroundServices
{
    public class CurrencyRateBackgroundService : BackgroundService
    {
        private readonly TimeSpan _period = TimeSpan.FromSeconds(30);
        private readonly ILogger<CurrencyRateBackgroundService> _logger;
        private readonly IServiceScopeFactory _factory;
        private int _executionCount = 0;
        public bool IsEnabled { get; set; }

        public CurrencyRateBackgroundService(
            ILogger<CurrencyRateBackgroundService> logger,
            IServiceScopeFactory factory)
        {
            _logger = logger;
            _factory = factory;
            IsEnabled = true;
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
                    if (IsEnabled)
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
using WebScraper.Application.CurrencyRates.Dtos;

namespace WebScraper.Application.CurrencyRates.Services
{
    public interface ICurrencyRateScraperService
    {
        CurrencyRateDto GetCurrencyRate();
        void DriverQuit();
    }
}

using ConsoleTables;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using WebScraper.Application.MoneyRates.Dtos;
using WebScraper.Application.SeedWorks;

namespace WebScraper.Application.MoneyRates.Services
{
    public class MoneyRateScraperService : BaseScraperService
    {
        private readonly ILogger<MoneyRateScraperService> _logger;
        public MoneyRateScraperService(ILogger<MoneyRateScraperService> logger)
        {
            _logger = logger;
        }
        public List<MoneyRateDto> GetMoneyRates()
        {
            _logger.LogInformation("Start scraping money rate...");

            var moneyRates = new List<MoneyRateDto>();

            driver.Navigate().GoToUrl(Url);
            Thread.Sleep(TreadSleep);

            var moneyRateList = driver.FindElements(By.XPath("//*[@id=\"root\"]/div[1]/div[2]/div/div/div[2]/div/div/div[1]/div/div/div[2]/div"));
            foreach (var row in moneyRateList)
            {
                var moneyRate = CreateMoneyRateDto(row);
                moneyRates.Add(moneyRate);
            }

            ShowConsoleTable(moneyRates);

            return moneyRates;
        }

        private static MoneyRateDto CreateMoneyRateDto(IWebElement row)
        {
            return new MoneyRateDto()
            {
                Name = row.FindElement(By.XPath("div[1]")).Text,
                Symbol = row.FindElement(By.XPath("div[2]")).Text,
                Buy = Convert.ToDecimal(row.FindElement(By.XPath("div[3]")).Text),
                Sell = Convert.ToDecimal(row.FindElement(By.XPath("div[4]")).Text),
                CurrentTime = DateTime.Now.ToShortTimeString(),
                CurrentDate = DateTime.Now.ToShortDateString()
            };
        }
        private static void ShowConsoleTable(List<MoneyRateDto> moneyRates)
        {
            var table = new ConsoleTable("Name", "Symbol", "Sell", "Buy", "CurrentTime", "CurrentDate");

            foreach (var moneyRate in moneyRates)
                table.AddRow(moneyRate.Name, moneyRate.Symbol, moneyRate.Sell, moneyRate.Buy,
                             moneyRate.CurrentTime, moneyRate.CurrentDate);

            Console.WriteLine(table.ToString());
        }


    }
}

using ConsoleTables;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebScraper.Application.MoneyRates.Dtos;

namespace WebScraper.Application.MoneyRates.Services
{
    public class MoneyRateScraperService
    {
        private IWebDriver driver;
        private static string Url = "https://mex.co.ir";
        private static int TreadSleep = 5000;
        private readonly ILogger<MoneyRateScraperService> _logger;
        public MoneyRateScraperService(ILogger<MoneyRateScraperService> logger)
        {
            SetChromeOption();
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

        public void DriverQuit()
        {
            driver.Quit();
        }

        private void SetChromeOption()
        {
            var options = new ChromeOptions();
            options.AddArgument("--ignore_ssl");
            options.AddArguments("--ignore-ssl-errors");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArguments("headless");
            driver = new ChromeDriver(options);
        }
        private static MoneyRateDto CreateMoneyRateDto(IWebElement row)
        {
            return new MoneyRateDto()
            {
                Name = row.FindElement(By.XPath("div[1]")).Text,
                Symbol = row.FindElement(By.XPath("div[2]")).Text,
                Buy = Convert.ToDecimal(row.FindElement(By.XPath("div[3]")).Text),
                Sell = Convert.ToDecimal(row.FindElement(By.XPath("div[4]")).Text),
                CurrentTime = TimeOnly.FromDateTime(DateTime.Now),
                CurrentDate = DateOnly.FromDateTime(DateTime.Now)
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

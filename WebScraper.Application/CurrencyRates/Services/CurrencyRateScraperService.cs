using ConsoleTables;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebScraper.Application.CurrencyRates.Dtos;

namespace WebScraper.Application.CurrencyRates.Services
{
    public class CurrencyRateScraperService
    {
        private IWebDriver driver;
        private static string Url = "https://mex.co.ir";
        private static int TreadSleep = 5000;
        private readonly ILogger<CurrencyRateScraperService> _logger;
        public CurrencyRateScraperService(ILogger<CurrencyRateScraperService> logger)
        {
            SetChromeOption();
            _logger = logger;
        }

        public CurrencyRateDto GetCurrencyRate()
        {
            _logger.LogInformation("Start scraping currency rate...");

            IWebElement viewRate = ViewMex();

            ViewXeCom(viewRate);

            string rate = GetRate();

            var finalCurrencyRate = CreateCurrencyRateDto(rate);
            _logger.LogInformation($"Currency rate: {rate}, Date: {finalCurrencyRate.CurrentDate}, Time: {finalCurrencyRate.CurrentTime}");

            ShowConsoleTable(finalCurrencyRate);

            return finalCurrencyRate;
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
        private string GetRate()
        {
            var convertButton = driver.FindElement(By.XPath("//*[@id=\"__next\"]/div[3]/div[2]/section/div[2]/div/main/div/div[2]/button"));
            convertButton.Click();
            Thread.Sleep(TreadSleep);

            var rate = driver.FindElement(By.XPath("//*[@id=\"__next\"]/div[3]/div[2]/section/div[2]/div/main/div/div[2]/div[1]/div/p")).Text;
            return rate;
        }
        private void ViewXeCom(IWebElement viewRate)
        {
            var navigatUrl = viewRate.GetAttribute("href");
            driver.Navigate().GoToUrl(navigatUrl);
            driver.Manage().Cookies.AddCookie(new Cookie("lastConversion", "{%22amount%22:1%2C%22fromCurrency%22:%22USD%22%2C%22toCurrency%22:%22IRR%22}"));
            driver.Navigate().Refresh();
            Thread.Sleep(TreadSleep);
        }
        private IWebElement ViewMex()
        {
            driver.Navigate().GoToUrl(Url);
            Thread.Sleep(TreadSleep);
            var viewRate = driver.FindElement(By.XPath("//*[@id=\"root\"]/div[1]/div[2]/div/div/div[3]/div/div[3]/a"));
            return viewRate;
        }
        private static CurrencyRateDto CreateCurrencyRateDto(string rate)
        {
            return new CurrencyRateDto()
            {
                Rate = Convert.ToDecimal(rate.ToString().Split(' ')[3]),
                Symbol = rate.ToString().Split(' ')[4],
                CurrentDate = DateOnly.FromDateTime(DateTime.Now),
                CurrentTime = TimeOnly.FromDateTime(DateTime.Now)
            };
        }
        private static void ShowConsoleTable(CurrencyRateDto finalCurrencyRate)
        {
            var table = new ConsoleTable("CurrentTime", "CurrentDate", "Rate", "Symbol");
            table.AddRow(finalCurrencyRate.CurrentTime, finalCurrencyRate.CurrentDate,
                         finalCurrencyRate.Rate, finalCurrencyRate.Symbol);
            Console.WriteLine(table.ToString());
        }
    }
}

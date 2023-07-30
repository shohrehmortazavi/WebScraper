using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebScraper.Application.SeedWorks
{
    public class BaseScraperService
    {
        public IWebDriver driver;
        public readonly static string Url = "https://mex.co.ir";
        public readonly static int TreadSleep = 5000;

        public BaseScraperService()
        {
            SetChromeOption();
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
    }
}

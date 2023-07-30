namespace WebScraper.Application.SeedWorks
{
    public class BackgroundServicesSetting
    {
        public CurrencyRateSetting CurrencyRate { get; set; }
        public MoneyRateSetting MoneyRate { get; set; }
    }

    public class MoneyRateSetting: BaseSetting
    {
    }

    public class CurrencyRateSetting: BaseSetting
    {
    }

    public class BaseSetting
    {
        public bool IsEnabled { get; set; }
        public int RepeatedTime { get; set; }
    }
}

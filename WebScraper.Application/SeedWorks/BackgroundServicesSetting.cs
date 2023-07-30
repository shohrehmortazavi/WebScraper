namespace WebScraper.Application.SeedWorks
{
    public class BackgroundServicesSetting
    {
        public CurrencyRateSetting CurrencyRateSetting { get; set; }
        public MoneyRateSetting MoneyRateSetting { get; set; }
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

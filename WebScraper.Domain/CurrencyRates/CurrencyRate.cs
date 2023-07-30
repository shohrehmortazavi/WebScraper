using WebScraper.Domain.Share;

namespace WebScraper.Domain.CurrencyRates
{
    public class CurrencyRate : Entity
    {
        public string Symbol { get; private set; }
        public decimal Rate { get; private set; }
        public TimeOnly CurrentTime { get; private set; }
        public DateOnly CurrentDate { get; private set; }
        private CurrencyRate()
        {

        }
        public CurrencyRate(string symbol, decimal rate, TimeOnly currentTime, DateOnly currentDate)
        {
            Id = Guid.NewGuid();
            Symbol = symbol;
            Rate = rate;
            CurrentTime = currentTime;
            CurrentDate = currentDate;
            CreatedDate = DateTime.Now;
        }
    }
}

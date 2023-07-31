using WebScraper.Domain.Share;

namespace WebScraper.Domain.CurrencyRates
{
    public class CurrencyRate : Entity
    {
        public string Symbol { get; private set; }
        public decimal Rate { get; private set; }
        public DateTime CurrentDate { get; private set; }
        private CurrencyRate()
        {

        }
        public CurrencyRate(string symbol, decimal rate, DateTime currentDate)
        {
            Id = Guid.NewGuid();
            Symbol = symbol;
            Rate = rate;
            CurrentDate = currentDate;
            CreatedDate = DateTime.Now;
        }
    }
}

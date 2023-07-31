using WebScraper.Domain.Share;

namespace WebScraper.Domain.MoneyRates
{
    public class MoneyRate : Entity
    {
        public string Name { get; private set; }
        public string Symbol { get; private set; }
        public decimal Sell { get; private set; }
        public decimal Buy { get; private set; }
        public DateTime CurrentDate { get; private set; }
        private MoneyRate()
        {

        }
        public MoneyRate(string name, string symbol,
                         decimal sell, decimal buy, DateTime currentDate)
        {
            Id = Guid.NewGuid();
            Name = name;
            Symbol = symbol;
            Sell = sell;
            Buy = buy;
            CurrentDate = currentDate;
            CreatedDate = DateTime.Now;
        }
    }
}

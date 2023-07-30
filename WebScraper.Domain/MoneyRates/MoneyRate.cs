using WebScraper.Domain.Share;

namespace WebScraper.Domain.MoneyRates
{
    public class MoneyRate : Entity
    {
        public string Name { get; private set; }
        public string Symbol { get; private set; }
        public decimal Sell { get; private set; }
        public decimal Buy { get; private set; }
        public TimeOnly CurrentTime { get; private set; }
        public DateOnly CurrentDate { get; private set; }
        private MoneyRate()
        {

        }
        public MoneyRate(string name, string symbol,
                         decimal sell, decimal buy,
                         TimeOnly currentTime, DateOnly currentDate)
        {
            Id = Guid.NewGuid();
            Name = name;
            Symbol = symbol;
            Sell = sell;
            Buy = buy;
            CurrentTime = currentTime;
            CurrentDate = currentDate;
            CreatedDate = DateTime.Now;
        }
    }
}

namespace WebScraper.Application.MoneyRates.Dtos
{
    public class MoneyRateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public decimal Sell { get; set; }
        public decimal Buy { get; set; }
        public TimeOnly CurrentTime { get; set; }
        public DateOnly CurrentDate { get; set; }



    }
}

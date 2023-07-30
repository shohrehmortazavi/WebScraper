namespace WebScraper.Application.CurrencyRates.Dtos
{
    public class CurrencyRateDto
    {
        public Guid Id { get; set; }
        public string Symbol { get; set; }
        public decimal Rate { get; set; }
        public TimeOnly CurrentTime { get; set; }
        public DateOnly CurrentDate { get; set; }

    }
}

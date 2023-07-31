namespace WebScraper.Application.MoneyRates.Dtos
{
    public class MoneyRateAverageResponseDto
    {
        public string Name { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public decimal AverageRate { get; set; }
        public string MoneyRateType { get; set; }
    }
}

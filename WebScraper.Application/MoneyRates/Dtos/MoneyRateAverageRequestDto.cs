using WebScraper.Domain.Share.Enums;

namespace WebScraper.Application.MoneyRates.Dtos
{
    public class MoneyRateAverageRequestDto
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public MoneyRateTypeEnum MoneyRateType { get; set; }
    }
}

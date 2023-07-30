using WebScraper.DataAccess.SeedWorks;
using WebScraper.Domain.CurrencyRates;
using WebScraper.Domain.Share;

namespace WebScraper.DataAccess.CurrencyRates
{
    public class CurrencyRateWriteRepository : MongoWriteRepository<CurrencyRate>, ICurrencyRateWriteRepository
    {
        public CurrencyRateWriteRepository(IMongoContext context) : base(context)
        {
        }
    }
}

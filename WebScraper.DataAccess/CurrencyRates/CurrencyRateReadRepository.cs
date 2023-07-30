using WebScraper.DataAccess.SeedWorks;
using WebScraper.Domain.CurrencyRates;
using WebScraper.Domain.Share;

namespace WebScraper.DataAccess.CurrencyRates
{
    public class CurrencyRateReadRepository : MongoReadRepository<CurrencyRate>, ICurrencyRateReadRepository
    {
        public CurrencyRateReadRepository(IMongoContext context) : base(context)
        {
        }
    }
}

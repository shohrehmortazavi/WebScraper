using WebScraper.DataAccess.SeedWorks;
using WebScraper.Domain.MoneyRates;
using WebScraper.Domain.Share;

namespace WebScraper.DataAccess.MoneyRates
{
    public class MoneyRateReadRepository : MongoReadRepository<MoneyRate>, IMoneyRateReadRepository
    {
        public MoneyRateReadRepository(IMongoContext context) : base(context)
        {
        }
    }
}

using WebScraper.DataAccess.SeedWorks;
using WebScraper.Domain.MoneyRates;
using WebScraper.Domain.Share;

namespace WebScraper.DataAccess.MoneyRates
{
    public class MoneyRateWriteRepository : MongoWriteRepository<MoneyRate>, IMoneyRateWriteRepository
    {
        public MoneyRateWriteRepository(IMongoContext context) : base(context)
        {
        }
    }
}

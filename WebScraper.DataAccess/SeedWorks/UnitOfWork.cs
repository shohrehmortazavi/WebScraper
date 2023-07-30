using WebScraper.DataAccess.CurrencyRates;
using WebScraper.Domain.CurrencyRates;
using WebScraper.Domain.CurrencyRates.SeedWorks;
using WebScraper.Domain.Share;

namespace WebScraper.DataAccess.SeedWorks
{
    public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
    {
        public UnitOfWork(IMongoContext context) : base(context)
        {

        }

        private ICurrencyRateReadRepository _currencyRateReadRepository;
        public ICurrencyRateReadRepository CurrencyRateReadRepository
        {
            get => _currencyRateReadRepository ?? new CurrencyRateReadRepository(MongoContext());
        }

        private ICurrencyRateWriteRepository _currencyRateWriteRepository;
        public ICurrencyRateWriteRepository CurrencyRateWriteRepository
        {
            get => _currencyRateWriteRepository ?? new CurrencyRateWriteRepository(MongoContext());
        }
    }
}

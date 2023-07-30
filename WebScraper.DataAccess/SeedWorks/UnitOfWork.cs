using WebScraper.DataAccess.CurrencyRates;
using WebScraper.DataAccess.MoneyRates;
using WebScraper.Domain.CurrencyRates;
using WebScraper.Domain.MoneyRates;
using WebScraper.Domain.SeedWorks;
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


        private IMoneyRateReadRepository _moneyRateReadRepository;
        public IMoneyRateReadRepository MoneyRateReadRepository
        {
            get => _moneyRateReadRepository ?? new MoneyRateReadRepository(MongoContext());
        }

        private IMoneyRateWriteRepository _moneyRateWriteRepository;
        public IMoneyRateWriteRepository MoneyRateWriteRepository
        {
            get => _moneyRateWriteRepository ?? new MoneyRateWriteRepository(MongoContext());
        }
    }
}

using WebScraper.Domain.CurrencyRates;
using WebScraper.Domain.MoneyRates;
using WebScraper.Domain.Share;

namespace WebScraper.Domain.SeedWorks
{
    public interface IUnitOfWork : IBaseUnitOfWork
    {
        public ICurrencyRateReadRepository CurrencyRateReadRepository { get; }
        public ICurrencyRateWriteRepository CurrencyRateWriteRepository { get; }

        public IMoneyRateReadRepository MoneyRateReadRepository { get; }
        public IMoneyRateWriteRepository MoneyRateWriteRepository { get; }
    }
}

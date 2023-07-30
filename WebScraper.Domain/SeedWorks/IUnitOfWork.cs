using WebScraper.Domain.Share;

namespace WebScraper.Domain.CurrencyRates.SeedWorks
{
    public interface IUnitOfWork : IBaseUnitOfWork
    {
        public ICurrencyRateReadRepository CurrencyRateReadRepository { get; }
        public ICurrencyRateWriteRepository CurrencyRateWriteRepository { get; }
    }
}

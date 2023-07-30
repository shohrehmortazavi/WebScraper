using WebScraper.Domain.Share;

namespace WebScraper.DataAccess.SeedWorks
{
    public class BaseUnitOfWork : IBaseUnitOfWork
    {
        private readonly IMongoContext _context;

        public BaseUnitOfWork(IMongoContext context)
        {
            _context = context;
        }

        public IMongoContext MongoContext() => _context;

        public async Task<bool> Commit()
        {
            var changeAmount = await _context.SaveChanges();

            return changeAmount > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

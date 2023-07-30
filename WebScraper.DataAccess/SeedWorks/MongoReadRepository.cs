using MongoDB.Driver;
using System.Linq.Expressions;
using WebScraper.Domain.Share;

namespace WebScraper.DataAccess.SeedWorks
{

    public class MongoReadRepository<T> : IReadRepository<T> where T : Entity
    {
        protected readonly IMongoContext _context;
        protected readonly IMongoCollection<T> _dbCollection;
        private readonly FilterDefinitionBuilder<T> _filterBuilder = Builders<T>.Filter;

        public MongoReadRepository(IMongoContext context)
        {
            _context = context;
            _dbCollection = context.GetCollection<T>(typeof(T).Name);
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            var result = await _dbCollection.Find(x => true).ToListAsync();
            return result;
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            var finalFilter = Expression.Lambda<Func<T, bool>>(filter, filter.Parameters);

            var result = await _dbCollection.Find(finalFilter).ToListAsync();
            return result;
        }

        public async Task<T> GetAsync(Guid id)
        {
            FilterDefinition<T> filter = _filterBuilder.Eq(entity => entity.Id, id);
            var result = await _dbCollection.Find(filter).FirstOrDefaultAsync();
            return result;
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbCollection.Find(filter).FirstOrDefaultAsync();
        }

    }
}
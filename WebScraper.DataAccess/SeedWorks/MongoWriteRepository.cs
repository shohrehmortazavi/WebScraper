using MongoDB.Driver;
using WebScraper.Domain.Share;

namespace WebScraper.DataAccess.SeedWorks
{

    public class MongoWriteRepository<T> : IWriteRepository<T> where T : Entity
    {
        private readonly IMongoCollection<T> dbCollection;
        private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;
        protected readonly IMongoContext _context;

        public MongoWriteRepository(IMongoContext context)
        {
            _context = context;
            dbCollection = context.GetCollection<T>(typeof(T).Name);
        }

        public async Task CreateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.AddCommand(async () => await dbCollection.InsertOneAsync(entity));
      
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            FilterDefinition<T> filter = filterBuilder.Eq(existingEntity => existingEntity.Id, entity.Id);
            _context.AddCommand(async () => await dbCollection.ReplaceOneAsync(filter, entity));
      
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(Guid id)
        {
            FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.Id, id);
            _context.AddCommand(async () => await dbCollection.DeleteOneAsync(filter));
     
            await Task.CompletedTask;
        }
    }
}
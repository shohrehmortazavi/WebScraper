using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using WebScraper.Domain.Share;

namespace WebScraper.DataAccess.SeedWorks
{
    public class MongoContext : IMongoContext
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase _database;
        private readonly MongoClient _mongoClient;
        private readonly List<Func<Task>> _commands;
        private readonly MongoDbSettings _mongoDbSettings;
        private IClientSessionHandle _session;

        public MongoContext(IConfiguration configuration)
        {
            _configuration = configuration;

            // Every command will be stored and it'll be processed at SaveChanges
            _commands = new List<Func<Task>>();

            _mongoDbSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();

            if (_mongoClient != null)
                return;

            _mongoClient = new MongoClient(_mongoDbSettings.ConnectionString);
            _database = _mongoClient.GetDatabase(_mongoDbSettings.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }

        public void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }
        public async Task<int> SaveChanges()
        {
            int commandCount = 0;

            //  using (_session = await _mongoClient.StartSessionAsync())
            //  {
           // _session.StartTransaction();
            var commandTasks = _commands.Select(c => c());
            await Task.WhenAll(commandTasks);

            //   await _session.CommitTransactionAsync();
            //    commandCount = _commands.Count;
            _commands.Clear();

            //   }
            return commandCount;
        }
        public void Dispose()
        {
            _session?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using WebScraper.Domain.Share;

namespace WebScraper.DataAccess.SeedWorks
{
    public static class Extensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            services.AddSingleton(serviceProvider =>
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                var mongoDbSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
                var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);
             
                return mongoClient.GetDatabase(mongoDbSettings.DatabaseName);
            });
            services.AddSingleton<IMongoContext, MongoContext>();
            services.AddTransient<IBaseUnitOfWork, BaseUnitOfWork>();
            return services;
        }

        public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services)
            where T : Entity
        {
            services.AddSingleton<IReadRepository<T>>(serviceProvider =>
            {
                var context = serviceProvider.GetService<IMongoContext>();
                return new MongoReadRepository<T>(context);
            });

            services.AddSingleton<IWriteRepository<T>>(serviceProvider =>
            {
                var context = serviceProvider.GetService<IMongoContext>();
                return new MongoWriteRepository<T>(context);
            });

            return services;
        }
    }

}
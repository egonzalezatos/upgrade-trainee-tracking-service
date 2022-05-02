using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Upgrade.TraineeTracking.NonRelational.Configurations
{
    public class MongoDbConnection : IMongoDbConnection
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        
        public MongoDbConnection(IConfiguration configuration)
        {
            _client = new MongoClient(configuration.GetConnectionString("DbConnect"));
            _database = _client.GetDatabase(configuration.GetConnectionString("DbName"));
        }

        public IMongoClient GetClient()
        {
            return _client;
        }

        public IMongoDatabase GetDatabase()
        {
            return _database;
        }
    }
}
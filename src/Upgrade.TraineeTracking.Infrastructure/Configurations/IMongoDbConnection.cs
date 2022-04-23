using MongoDB.Driver;

namespace Upgrade.TraineeTracking.Infrastructure.Configurations
{
    public interface IMongoDbConnection
    {
        public IMongoDatabase GetDatabase();
        public IMongoClient GetClient();
    }
}
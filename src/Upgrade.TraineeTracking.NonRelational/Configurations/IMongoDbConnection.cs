using MongoDB.Driver;

namespace Upgrade.TraineeTracking.NonRelational.Configurations
{
    public interface IMongoDbConnection
    {
        public IMongoDatabase GetDatabase();
        public IMongoClient GetClient();
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Sdk.Domain.Models;
using Upgrade.TraineeTracking.Domain.Repositories;
using Upgrade.TraineeTracking.NonRelational.Configurations;

namespace Upgrade.TraineeTracking.NonRelational.Repositories
{
    public abstract class Repository<TDocument, TKey> : IRepository<TDocument, TKey> where TDocument : Entity<TKey>
    {
        protected readonly IMongoCollection<TDocument> Collection;
        protected readonly IMongoDatabase Db;
        public abstract string CollectionName { get; }
        public Repository(IMongoDbConnection connection)
        {
            Db = connection.GetDatabase();
            Collection = Db.GetCollection<TDocument>(CollectionName);
        }

        public async Task<List<TDocument>> GetAsync() =>
            await Collection.Find(_ => true).ToListAsync();

        public async Task<TDocument?> GetAsync(TKey id) =>
            await Collection.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();

        public async Task CreateAsync(TDocument t) =>
            await Collection.InsertOneAsync(t);

        public async Task UpdateAsync(TKey id, TDocument t) =>
            await Collection.ReplaceOneAsync(x => x.Id.Equals(id), t);

        public async Task RemoveAsync(TKey id) =>
            await Collection.DeleteOneAsync(x => x.Id.Equals(id));
    }
}
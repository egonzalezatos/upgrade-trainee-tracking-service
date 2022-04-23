using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Upgrade.TraineeTracking.Domain.Models;
using Upgrade.TraineeTracking.Domain.Repositories;
using Upgrade.TraineeTracking.Infrastructure.Configurations;

namespace Upgrade.TraineeTracking.Infrastructure.Repositories
{
    public abstract class Repository<TDocument> : IRepository<TDocument> where TDocument : Identifiable
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

        public async Task<TDocument?> GetAsync(string id) =>
            await Collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(TDocument document) =>
            await Collection.InsertOneAsync(document);

        public async Task UpdateAsync(string id, TDocument updatedDocument) =>
            await Collection.ReplaceOneAsync(x => x.Id == id, updatedDocument);

        public async Task RemoveAsync(string id) =>
            await Collection.DeleteOneAsync(x => x.Id == id);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Upgrade.TraineeTracking.Domain.Models;

namespace Upgrade.TraineeTracking.Domain.Repositories
{
    public interface IRepository {}
    public interface IRepository<TDocument> : IRepository where TDocument : Identifiable
    {
        public Task<List<TDocument>> GetAsync();
        public Task<TDocument?> GetAsync(string id);
        public Task CreateAsync(TDocument document);
        public Task UpdateAsync(string id, TDocument updatedDocument);
        public Task RemoveAsync(string id);

    }
}
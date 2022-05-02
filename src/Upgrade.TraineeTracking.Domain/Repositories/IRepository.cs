using System.Collections.Generic;
using System.Threading.Tasks;
using Sdk.Domain.Models;

namespace Upgrade.TraineeTracking.Domain.Repositories
{
    public interface IRepository {}
    public interface IRepository<T, TKey> : IRepository where T : Entity<TKey>
    {
        public Task<List<T>> GetAsync();
        public Task<T?> GetAsync(TKey id);
        public Task CreateAsync(T t);
        public Task UpdateAsync(TKey id, T t);
        public Task RemoveAsync(TKey id);
    }
}
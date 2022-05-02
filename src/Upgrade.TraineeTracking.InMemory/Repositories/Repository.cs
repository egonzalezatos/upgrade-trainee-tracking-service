using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sdk.Domain.Models;
using Upgrade.TraineeTracking.Domain.Repositories;

namespace Upgrade.TraineeTracking.InMemory.Repositories
{
    public class Repository<T, TKey> : IRepository<T, TKey> where T : Entity<TKey>
    {
        protected readonly DbContext Context;
        protected readonly DbSet<T> DbSet;
        public Repository(DbContext context)
        {
            Context = context ?? throw new ArgumentNullException(paramName: nameof(context));
            DbSet = Context.Set<T>();
        }

        public DbContext GetContext()
        {
            return Context;
        }

        public DbSet<T> GetEntitySet()
        {
            return DbSet;
        }

        public virtual async Task<List<T>> GetAsync()
        {
            List<T> list = await DbSet.ToListAsync();
            return list;
        }

        public virtual async Task<T> GetAsync(TKey id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task CreateAsync(T entity)
        {
            DbSet.Add(entity);
            await Context.SaveChangesAsync();
        }

        public virtual async Task RemoveAsync(TKey id)
        {
            T entity = await DbSet.FindAsync(id);
            if (entity != null)
                DbSet.Remove(entity);
            await Context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TKey id, T entity)
        {
            DbSet.Update(entity);
            await Context.SaveChangesAsync();
        }
    }
}
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Upgrade.TraineeTracking.InMemory.Extensions;
using Upgrade.TraineeTracking.Shared.Extensions;

namespace Upgrade.TraineeTracking.InMemory.Configurations
{
    public class InMemoryConfiguration
    {
        public static void Configure(IServiceCollection services, string database)
        {
            services.AddDbContext<DbContext, InMemoryContext>(opts => opts.UseInMemoryDatabase(database));
            services.AddRepositories();
        }
        
        public static void Seed(IServiceProvider serviceProvider)
        {
            DbContext context = serviceProvider.GetService<DbContext>();
            context.ThrowIfNull();
            context!.Database.EnsureCreated();
        }
    }
}
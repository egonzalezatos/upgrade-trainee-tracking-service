using System;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Upgrade.TraineeTracking.NonRelational.Designs;
using Upgrade.TraineeTracking.NonRelational.Extensions;
using Upgrade.TraineeTracking.NonRelational.Seeds;
using Upgrade.TraineeTracking.Shared.Extensions;

namespace Upgrade.TraineeTracking.NonRelational.Configurations
{
    public class NonRelationalConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<IMongoDbConnection, MongoDbConnection>();
            Designer.RunDesigns<string?>();
            services.AddRepositories();
        }

        public static void Seed(IServiceProvider serviceProvider)
        {
            IMongoDbConnection connection = serviceProvider.GetService<IMongoDbConnection>();
            connection.ThrowIfNull();
            MongoSeeder.Seed(connection!.GetDatabase());
        }

    }
}
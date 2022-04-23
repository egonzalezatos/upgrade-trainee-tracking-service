using Microsoft.Extensions.DependencyInjection;
using Upgrade.TraineeTracking.Domain.Repositories;
using Upgrade.TraineeTracking.Infrastructure.Configurations;
using Upgrade.TraineeTracking.Infrastructure.Designs;

namespace Upgrade.TraineeTracking.Infrastructure.Extensions
{
    public static class DependencyExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddContext();
            services.AddRepositories();
            return services;
        }

        public static IServiceCollection AddContext(this IServiceCollection services)
        {
            services.AddSingleton<IMongoDbConnection, MongoDbConnection>();
            Designer.RunDesigns();
            return services;
        }
        
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.Scan(selector => selector
                .FromAssembliesOf(typeof(IRepository), typeof(DependencyExtension))
                .AddClasses(classes => classes.Where( t=>!t.IsInterface && t.IsAssignableTo(typeof(IRepository)) && t.Name.EndsWith("Repository")))
            .AsMatchingInterface()
               .WithScopedLifetime());
            return services;
        }
    }
}
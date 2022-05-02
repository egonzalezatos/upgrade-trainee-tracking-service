using Microsoft.Extensions.DependencyInjection;
using Upgrade.TraineeTracking.Domain.Repositories;

namespace Upgrade.TraineeTracking.NonRelational.Extensions
{
    public static class DependencyExtension
    {
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
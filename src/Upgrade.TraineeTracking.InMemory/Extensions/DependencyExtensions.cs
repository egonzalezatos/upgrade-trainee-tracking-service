using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Upgrade.TraineeTracking.Domain.Repositories;

namespace Upgrade.TraineeTracking.InMemory.Extensions
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.Scan(selector => selector
                .FromAssembliesOf(typeof(IRepository), typeof(DependencyExtensions))
                .AddClasses(
                    classes => classes.Where( t=>!t.IsInterface && t.Name.EndsWith("Repository")))
                .AddClasses(
                    x=>x.Where(
                        t=>t.IsAssignableTo(typeof(IRepository)))
                    )
                .AsMatchingInterface()
                .WithScopedLifetime());
            return services;
        }
    }
}
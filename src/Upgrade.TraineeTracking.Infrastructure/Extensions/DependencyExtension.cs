using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Upgrade.TraineeTracking.InMemory.Configurations;
using Upgrade.TraineeTracking.NonRelational.Configurations;

namespace Upgrade.TraineeTracking.Infrastructure.Extensions
{
    public static class DependencyExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.IsInMemory())
                InMemoryConfiguration.Configure(services, database: "test");
            else if (configuration.IsNonRelational())
                NonRelationalConfiguration.Configure(services);
            return services;
        }

        public static IApplicationBuilder UseSeeds(this IApplicationBuilder app, IConfiguration configuration)
        {
            using (var scope = app.ApplicationServices.CreateScope())
                if (configuration.IsInMemory())
                    InMemoryConfiguration.Seed(scope.ServiceProvider);
                else if (configuration.IsNonRelational())
                    NonRelationalConfiguration.Seed(scope.ServiceProvider);
            return app;
        }
    }
}
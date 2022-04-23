using Microsoft.Extensions.DependencyInjection;
using Upgrade.TraineeTracking.Grpc.Extensions;
using Upgrade.TraineeTracking.Services.Extensions;

namespace Upgrade.TraineeTracking.IoC.Extensions
{
    public static class DependencyExtension
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddApplication();
            services.AddGrpc();
            return services;
        }
    }
}
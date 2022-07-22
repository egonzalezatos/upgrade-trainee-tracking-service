using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Upgrade.TraineeTracking.Grpc.Extensions;
using Upgrade.TraineeTracking.Services.Extensions;

namespace Upgrade.TraineeTracking.IoC.Extensions
{
    public static class DependencyExtension
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplication();
            services.AddGrpc(Convert.ToBoolean(configuration["REDIS_ENABLED"]));
            return services;
        }
    }
}
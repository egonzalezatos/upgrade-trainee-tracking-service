using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Upgrade.TraineeTracking.Options.ExternalApis;
using Upgrade.TraineeTracking.Options.grpc;

namespace Upgrade.TraineeTracking.Options.Extensions
{
    public static  class DependencyExtension
    {
        public static IServiceCollection LoadOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GrpcOptions>(configuration.GetSection(GrpcOptions.Key));
            services.Configure<HttpClientsOptions>(configuration.GetSection(HttpClientsOptions.Key));
            return services;
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using Upgrade.TraineeTracking.Grpc.Abstractions.Clients;
using Upgrade.TraineeTracking.Grpc.Clients;

namespace Upgrade.TraineeTracking.Grpc.Extensions
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddGrpc(this IServiceCollection services, bool enableRedis = false)
        {
            services.AddAutoMapper(typeof(DependencyExtensions));
            if (enableRedis) services.AddClientsWithCache();
            else services.AddClients();
            return services;
        }

        public static IServiceCollection AddClients(this IServiceCollection services)
        {
            services.AddScoped<IProfileManagementClient, ProfileManagementClient>();
            services.AddScoped<ICoursesServiceClient, CoursesServiceClient>();
            return services;
        }
        
        public static IServiceCollection AddClientsWithCache(this IServiceCollection services)
        {
            services.AddScoped<IProfileManagementClient, ProfileManagementClientCacheable>();
            services.AddScoped<ICoursesServiceClient, CoursesClientCacheable>();
            return services;
        }
    }
}
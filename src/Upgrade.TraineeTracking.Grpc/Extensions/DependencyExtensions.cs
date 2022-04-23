using Microsoft.Extensions.DependencyInjection;
using Upgrade.TraineeTracking.Grpc.Abstractions.Clients;
using Upgrade.TraineeTracking.Grpc.Clients;

namespace Upgrade.TraineeTracking.Grpc.Extensions
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddGrpc(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DependencyExtensions));
            services.AddClients();   
            return services;
        }

        public static IServiceCollection AddClients(this IServiceCollection services)
        {
            services.AddScoped<IProfileManagementClient, ProfileManagementClientCacheable>();
            services.AddScoped<ICoursesServiceClient, CoursesClientCacheable>();
            return services;
        }
    }
}
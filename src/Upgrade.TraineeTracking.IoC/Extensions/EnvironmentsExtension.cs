using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Upgrade.TraineeTracking.Grpc.Configurations;
using Upgrade.TraineeTracking.Grpc.Extensions;
using Upgrade.TraineeTracking.Infrastructure.Extensions;
using Upgrade.TraineeTracking.Redis.Extensions;

namespace Upgrade.TraineeTracking.IoC.Extensions
{
    public static class EnvironmentsExtension
    {
        public static IServiceCollection ReadConfigurationEnvironments(this IServiceCollection services, IConfiguration configuration)
        {
            configuration["ConnectionStrings:Redis"] = configuration.ReadRedisEnvironments();
            configuration["ConnectionStrings:DbConnect"] = configuration.ReadDbConnectionEnvironments();
            configuration["ConnectionStrings:DbName"] = configuration.ReadDbNameEnvironments();
            configuration[$"Grpc:Clients:{GrpcCodeNames.GRPC_COURSES}"] = configuration.ReadGrpcEnvironments(GrpcCodeNames.GRPC_COURSES);
            configuration[$"Grpc:Clients:{GrpcCodeNames.GRPC_PROFILE_MANAGEMENT}"] = configuration.ReadGrpcEnvironments(GrpcCodeNames.GRPC_PROFILE_MANAGEMENT);
            return services;
        }
    }
}
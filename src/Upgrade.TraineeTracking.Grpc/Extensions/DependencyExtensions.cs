using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Upgrade.TraineeTracking.Grpc.Abstractions.Clients;
using Upgrade.TraineeTracking.Grpc.Clients;
using Upgrade.TraineeTracking.Grpc.Configurations;
using Upgrade.TraineeTracking.Grpc.Interceptors;
using Upgrade.TraineeTracking.Options.grpc;
using Upgrade.TraineeTracking.Security.Services.Abstractions;

namespace Upgrade.TraineeTracking.Grpc.Extensions
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddGrpc(this IServiceCollection services, bool enableRedis = false)
        {
            services.AddAutoMapper(typeof(DependencyExtensions));
            services.AddInterceptors();
            var serviceProvider = services.BuildServiceProvider();
            services.AddGrpcClients(serviceProvider.GetService<IOptions<GrpcOptions>>()!.Value);
            
           // if (enableRedis) services.AddClientsWithCache();
            // else 
            services.AddClients();
            
            return services;
        }

        private static IServiceCollection AddGrpcClients(this IServiceCollection services, GrpcOptions grpc)
        {
            services.AddGrpcClient<GrpcProfileManagement.GrpcProfileManagementClient>(
                    cfg => cfg.Address = new Uri(grpc.Clients[GrpcCodeNames.GRPC_PROFILE_MANAGEMENT]))
                .AddInterceptor<LoggingInterceptor>()
                .AddCallCredentials(async (_, metadata, serviceProvider) =>
                {
                    var tokenProvider = serviceProvider.GetService<ITokenProvider>();
                    string token = await tokenProvider!.GetTokenAsync();
                    metadata.Add("Authorization", $"Bearer {token}");
                })
                .ConfigureChannel(o => o.UnsafeUseInsecureChannelCallCredentials = true);//TODO Remove this unsafe.
                
            services.AddGrpcClient<GrpcCourses.GrpcCoursesClient>(
                cfg => cfg.Address = new Uri(grpc.Clients[GrpcCodeNames.GRPC_COURSES]))
                .AddInterceptor<LoggingInterceptor>()
                .AddCallCredentials(async (_, metadata, serviceProvider) =>
                {
                    var tokenProvider = serviceProvider.GetService<ITokenProvider>();
                    metadata.Add("Authorization", $"Bearer {await tokenProvider!.GetTokenAsync()}");
                })
                .ConfigureChannel(o => o.UnsafeUseInsecureChannelCallCredentials = true);//TODO Remove this unsafe.
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

        internal static IServiceCollection AddInterceptors(this IServiceCollection services)
        {
            services.AddScoped<LoggingInterceptor>();
            return services;
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Upgrade.TraineeTracking.Redis.Extensions
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddRedis(this IServiceCollection services, string connectionString, string name)
        {
            //Redis
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = connectionString;
                options.InstanceName = name;
            });
            return services;
        }
    }
}
using System;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Upgrade.TraineeTracking.Redis.Extensions
{
    public static class RedisEnvironmentsExtension
    {
        public static IConfiguration ReadRedisEnvironments(this IConfiguration configuration)
        {
            configuration["ConnectionStrings:Redis"] = new StringBuilder()
                .Append($"{configuration["REDIS_HOST"]}:{configuration["REDIS_PORT"]}")
                .Append($",password={configuration["REDIS_PASS"]}")
                .ToString();
            Console.Out.WriteLine(configuration.GetConnectionString("ConnectionStrings:Redis"));
            return configuration;
        }        
        
        public static string GetRedis(this IConfiguration configuration)
        {
            return configuration["ConnectionStrings:Redis"];
        }
    }
}
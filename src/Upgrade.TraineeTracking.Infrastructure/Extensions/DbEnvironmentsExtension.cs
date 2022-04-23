using System;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Upgrade.TraineeTracking.Infrastructure.Extensions
{
    public static class DbEnvironmentsExtension
    {
        public static IConfiguration ReadDbEnvironments(this IConfiguration configuration)
        {
            configuration.ReadDbConnectionEnvironments();
            configuration.ReadDbNameEnvironments();
            return configuration;
        }

        private static IConfiguration ReadDbConnectionEnvironments(this IConfiguration configuration)
        {
            configuration[$"ConnectionStrings:DbConnect"] = new StringBuilder()
                .Append("mongodb://")
                .Append($"{configuration["DB_USERNAME"]}:{configuration["DB_PASSWORD"]}")
                .Append($"@{configuration["DB_SERVER"]}:{configuration["DB_PORT"]}")
                .ToString();
            Console.Out.WriteLine(configuration[$"ConnectionStrings:DbConnect"]);
            return configuration;
        }
        
        public static string GetDbConnection(this IConfiguration configuration)
        {
            return configuration[$"ConnectionStrings:DbConnect"];
        }

        private static IConfiguration ReadDbNameEnvironments(this IConfiguration configuration)
        {
            configuration["ConnectionStrings:DbName"] = new StringBuilder()
                .Append($"{configuration["DB_DATABASE"]}")
                .ToString();
            Console.Out.WriteLine(configuration["ConnectionStrings:DbName"]);
            return configuration;
        }
    }
}
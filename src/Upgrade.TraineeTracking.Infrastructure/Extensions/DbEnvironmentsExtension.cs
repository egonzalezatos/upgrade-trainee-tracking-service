using System;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Upgrade.TraineeTracking.Infrastructure.Extensions
{
    public static class DbEnvironmentsExtension
    {
        public static string ReadDbConnectionEnvironments(this IConfiguration configuration)
        {
            string @string = new StringBuilder()
                .Append("mongodb://")
                .Append($"{configuration["DB_USERNAME"]}:{configuration["DB_PASSWORD"]}")
                .Append($"@{configuration["DB_SERVER"]}:{configuration["DB_PORT"]}")
                .ToString();
            return @string;
        }
        
        public static string ReadDbNameEnvironments(this IConfiguration configuration)
        {
            var @string = new StringBuilder()
                .Append($"{configuration["DB_DATABASE"]}")
                .ToString();
            Console.Out.WriteLine(configuration["ConnectionStrings:DbName"]);
            return @string;
        }
    }
}
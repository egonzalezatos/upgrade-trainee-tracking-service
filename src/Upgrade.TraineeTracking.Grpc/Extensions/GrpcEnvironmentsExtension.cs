using System;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Upgrade.TraineeTracking.Grpc.Extensions
{
    public static class GrpcEnvironmentsExtension
    {
        public static string ReadGrpcEnvironments(this IConfiguration configuration, string grpcCode)
        {
            var @string = new StringBuilder()
                .Append($"http://")
                .Append($"{configuration[grpcCode + "_HOST"]}:")
                .Append($"{configuration[grpcCode + "_PORT"]}")
                .ToString();
            return @string;
        }
        
        public static string ReadGrpcServerEnvironments(this IConfiguration configuration)
        {
            var @string = new StringBuilder()
                .Append($"http://+:{configuration["GRPC_PORT"]}")
                .ToString();
            Console.Out.WriteLine(@string);
            return @string;
        }
    }
}
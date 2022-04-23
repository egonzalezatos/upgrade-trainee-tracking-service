using System;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Upgrade.TraineeTracking.Grpc.Extensions
{
    public static class GrpcEnvironmentsExtension
    {
        public static IConfiguration ReadGrpcEnvironments(this IConfiguration configuration, string grpcCode)
        {
            configuration[$"Grpc:{grpcCode}"] = new StringBuilder()
                .Append($"http://")
                .Append($"{configuration[grpcCode + "_HOST"]}:")
                .Append($"{configuration[grpcCode + "_PORT"]}")
                .ToString();
            Console.Out.WriteLine(configuration[$"Grpc:{grpcCode}"]);
            return configuration;
        }
        
        public static string GetGrpc(this IConfiguration configuration, string grpcCode)
        {
            return configuration[$"Grpc:{grpcCode}"];
        }
    }
}
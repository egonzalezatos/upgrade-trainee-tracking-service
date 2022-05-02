using Microsoft.Extensions.Configuration;

namespace Upgrade.TraineeTracking.Grpc.Extensions
{
    public static class ConfigurationExtension
    {
        public static string GetGrpc(this IConfiguration configuration, string grpcCode)
        {
            return configuration[$"Grpc:{grpcCode}"];
        }
    }
}
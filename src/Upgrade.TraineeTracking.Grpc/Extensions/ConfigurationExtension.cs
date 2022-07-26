using Microsoft.Extensions.Configuration;

namespace Upgrade.TraineeTracking.Grpc.Extensions
{
    public static class ConfigurationExtension
    {
        /// <summary>
        /// Deprecated
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="grpcCode"></param>
        /// <returns></returns>
        public static string GetGrpc(this IConfiguration configuration, string grpcCode)
        {
            return configuration[$"Grpc:Clients:{grpcCode}"];
        }
    }
}
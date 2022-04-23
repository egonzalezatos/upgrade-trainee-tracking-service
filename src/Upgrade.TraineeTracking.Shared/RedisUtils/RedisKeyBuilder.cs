using System.Text;

namespace Upgrade.TraineeTracking.Shared.RedisUtils
{
    public static class RedisKeyBuilder
    {
        public static string Key(string host, string service, string requestMethod, string reqParams)
        {
            return new StringBuilder(host + ":")
                .Append(service + ":")
                .Append(requestMethod + ":")
                .Append(reqParams)
                .ToString();
        }
        public static string Key(string service, string requestMethod, string reqParams)
        {
            return new StringBuilder()
                .Append(service + ":")
                .Append(requestMethod + ":")
                .Append(reqParams)
                .ToString();
        }
    }
}
using System.Collections.Generic;
using System.Text.Json;

namespace Upgrade.TraineeTracking.Shared.RedisUtils
{
    public static class ByteConverter
    {
        public static byte[] Array2Byte<T>(IEnumerable<T> obj)
        {
            return JsonSerializer.SerializeToUtf8Bytes(obj);
        }
        
        public static byte[] Array2Byte<T>(T obj)
        {
            return JsonSerializer.SerializeToUtf8Bytes(obj);
        }

        public static T Byte2Array<T>(byte[] data)
        {
            return JsonSerializer.Deserialize<T>(data)!;
        }
    }
}
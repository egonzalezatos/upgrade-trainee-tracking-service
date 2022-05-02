using System;

namespace Upgrade.TraineeTracking.Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static void ThrowIfNull(this object o)
        {
            if (o == null) throw new NullReferenceException(nameof(o));
        } 
    }
}
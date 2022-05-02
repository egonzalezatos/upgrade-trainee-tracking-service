using System;
using Microsoft.Extensions.Configuration;
using Upgrade.TraineeTracking.Infrastructure.Configurations;

namespace Upgrade.TraineeTracking.Infrastructure.Extensions
{
    public static class ConfigurationExtension
    {
        public static readonly string AppSetting = "PERSISTENCE_MODE";

        public static string GetPersistenceMode(this IConfiguration configuration)
        {
            if (configuration[AppSetting] == null)
                throw new ArgumentNullException(AppSetting);
            return configuration[AppSetting];
        }
        
        internal static bool IsInMemory(this IConfiguration configuration)
        {
            string persistenceMode = configuration.GetPersistenceMode();
            return persistenceMode.Equals(PersistenceModes.InMemory, StringComparison.OrdinalIgnoreCase);
        }
        
        internal static bool IsRelational(this IConfiguration configuration)
        {
            string persistenceMode = configuration.GetPersistenceMode();
            return persistenceMode.Equals(PersistenceModes.Relational, StringComparison.OrdinalIgnoreCase);
        }
        
        internal static bool IsNonRelational(this IConfiguration configuration)
        {
            string persistenceMode = configuration.GetPersistenceMode();
            return persistenceMode.Equals(PersistenceModes.NonRelational, StringComparison.OrdinalIgnoreCase);
        }
    }
}


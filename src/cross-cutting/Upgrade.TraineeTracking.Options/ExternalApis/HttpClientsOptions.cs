using System.Collections.Generic;

namespace Upgrade.TraineeTracking.Options.ExternalApis
{
    public class HttpClientsOptions
    {
        public const string Key = "Http";
        public Dictionary<string, string> Clients { get; set; }
    }
}
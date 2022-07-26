using System.Collections.Generic;

namespace Upgrade.TraineeTracking.Options.grpc
{
    public class GrpcOptions
    {
        public const string Key = "Grpc";
        public bool Enabled { get; set; }
        public Dictionary<string, string> Clients { get; set; }
    }
}
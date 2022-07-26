using System.Threading.Tasks;

namespace Upgrade.TraineeTracking.Security.Services.Abstractions
{
    public interface ITokenProvider
    {
        public Task<string> GetTokenAsync();
    }
}
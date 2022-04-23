using System.Collections.Generic;
using System.Threading.Tasks;
using Upgrade.TraineeTracking.Domain.Models;

namespace Upgrade.TraineeTracking.Domain.Repositories
{
    public interface IUserPlanRepository : IRepository<UserPlan>
    {
        public Task<List<UserPlan>> FindByUserIdAndProfileId(int userId, int profileId);
        
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Upgrade.TraineeTracking.Domain.Models;
using Upgrade.TraineeTracking.Domain.Repositories;

namespace Upgrade.TraineeTracking.InMemory.Repositories
{
    public class UserPlanRepository : Repository<UserPlan, string>, IUserPlanRepository
    {
        public UserPlanRepository(DbContext context) : base(context)
        {
        }

        public async Task<List<UserPlan>> FindByUserIdAndProfileId(int userId, int profileId)
        {
            var results = await DbSet.Where(d => d.UserId == userId && d.JobProfileId == profileId).ToListAsync();
            return results;
        }
    }
}
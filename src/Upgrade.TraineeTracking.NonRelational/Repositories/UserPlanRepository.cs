using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Upgrade.TraineeTracking.Domain.Models;
using Upgrade.TraineeTracking.Domain.Repositories;
using Upgrade.TraineeTracking.NonRelational.Configurations;

namespace Upgrade.TraineeTracking.NonRelational.Repositories
{
    public class UserPlanRepository : Repository<UserPlan, string>, IUserPlanRepository
    {
        public static string CollectionNameStatic = "user_plans";
        public override string CollectionName { get; } = CollectionNameStatic;
        
        public UserPlanRepository(IMongoDbConnection connection) : base(connection)
        {
        }

        public async Task<List<UserPlan>> FindByUserIdAndProfileId(int userId, int profileId)
        {
            var results = await Collection.FindAsync(d => d.UserId == userId && d.JobProfileId == profileId);
            return results.ToList();
        }
    }
}
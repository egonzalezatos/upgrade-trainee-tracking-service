using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Upgrade.TraineeTracking.Domain.Models;
using Upgrade.TraineeTracking.Domain.Repositories;
using Upgrade.TraineeTracking.Infrastructure.Configurations;

namespace Upgrade.TraineeTracking.Infrastructure.Repositories
{
    public class UserCoursesRepository : Repository<UserCourses>, IUserCoursesRepository
    {
        public static string CollectionNameStatic = "user_courses";
        public override string CollectionName { get; } = CollectionNameStatic;

        public UserCoursesRepository(IMongoDbConnection connection) : base(connection)
        {
        }
        
        public async Task<List<UserCourses>> FindByUserId(int userId)
        {
            var results = await Collection.FindAsync(d => d.UserId == userId);
            return results.ToList();
        }

        public async Task<List<UserCourses>> GetByUserIdAndProfileId(int userId, int profileId)
        {
            var plans = Collection.Database.GetCollection<UserPlan>(UserPlanRepository.CollectionNameStatic);
            var userCourses = plans.AsQueryable()
                .Where(plan => plan.UserId == userId && plan.JobProfileId == profileId)
                .Join(Collection, plan => plan.UserId, course => course.UserId, (plan, course) => course);
            return await userCourses.ToListAsync(CancellationToken.None);
        }
    }
}
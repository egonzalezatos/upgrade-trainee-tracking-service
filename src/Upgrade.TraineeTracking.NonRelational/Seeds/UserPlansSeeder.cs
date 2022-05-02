using MongoDB.Driver;
using Upgrade.TraineeTracking.Domain.Models;

namespace Upgrade.TraineeTracking.NonRelational.Seeds
{
    public class UserPlansSeeder
    {
        private static object[] data =
        {
            new UserPlan { JobProfileId = 1, UserId = 1, CourseId = 1 },
            new UserPlan { JobProfileId = 1, UserId = 1, CourseId = 2 },
            new UserPlan { JobProfileId = 1, UserId = 1, CourseId = 3 },
            new UserPlan { JobProfileId = 2, UserId = 2, CourseId = 1 },
            new UserPlan { JobProfileId = 2, UserId = 2, CourseId = 2 },
            new UserPlan { JobProfileId = 2, UserId = 2, CourseId = 3 },
        };
        
        public static void Seed(IMongoCollection<UserPlan> collection)
        {
            foreach (UserPlan userPlan in data)
            {
                if (!collection.Find(c => c.UserId == userPlan.UserId && c.CourseId == userPlan.CourseId && c.JobProfileId == userPlan.JobProfileId).Any())
                    collection.InsertOneAsync(userPlan);
            }
        }
    }
}
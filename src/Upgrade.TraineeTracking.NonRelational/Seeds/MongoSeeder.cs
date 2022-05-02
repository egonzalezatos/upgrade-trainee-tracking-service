using MongoDB.Driver;
using Upgrade.TraineeTracking.Domain.Models;
using Upgrade.TraineeTracking.NonRelational.Repositories;

namespace Upgrade.TraineeTracking.NonRelational.Seeds
{
    public class MongoSeeder
    {
        public static void Seed(IMongoDatabase database)
        {
            UserCoursesSeeder.Seed(database.GetCollection<UserCourses>(UserCoursesRepository.CollectionNameStatic));
            UserPlansSeeder.Seed(database.GetCollection<UserPlan>(UserPlanRepository.CollectionNameStatic));
        }
    }
}
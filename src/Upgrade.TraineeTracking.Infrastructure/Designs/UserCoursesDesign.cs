using MongoDB.Bson.Serialization;
using Upgrade.TraineeTracking.Domain.Models;

namespace Upgrade.TraineeTracking.Infrastructure.Designs
{
    public class UserCoursesDesign : ModelDesign<UserCourses>
    {
        public override void Design(BsonClassMap<UserCourses> builder)
        {
            
        }
    }
}
using MongoDB.Bson.Serialization;
using Upgrade.TraineeTracking.Domain.Models;

namespace Upgrade.TraineeTracking.Infrastructure.Designs
{
    public class UserPlanDesign : ModelDesign<UserPlan>
    {
        public override void Design(BsonClassMap<UserPlan> builder)
        {
            
        }
    }
}
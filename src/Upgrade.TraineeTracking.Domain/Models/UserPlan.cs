using Sdk.Domain.Models;

namespace Upgrade.TraineeTracking.Domain.Models
{
    public class UserPlan : Entity<string?>
    {
        public int UserId { get; set; }
        public int JobProfileId { get; set; }
        public int CourseId { get; set; }
    }
}
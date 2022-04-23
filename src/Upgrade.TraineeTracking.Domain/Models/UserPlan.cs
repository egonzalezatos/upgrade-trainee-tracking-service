namespace Upgrade.TraineeTracking.Domain.Models
{
    public class UserPlan : Identifiable
    {
        public int UserId { get; set; }
        public int JobProfileId { get; set; }
        public int CourseId { get; set; }
    }
}
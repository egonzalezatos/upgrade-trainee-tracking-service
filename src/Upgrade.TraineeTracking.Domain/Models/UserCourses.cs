using Sdk.Domain.Models;

namespace Upgrade.TraineeTracking.Domain.Models
{
    public class UserCourses : Entity<string?>
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public decimal Percentage { get; set; }
        public bool Completed { get; set; }
    }
}
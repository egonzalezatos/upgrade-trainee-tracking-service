namespace Upgrade.TraineeTracking.DTO.DTOs
{
    public class UserPlanDto
    {
        public int UserId { get; set; }
        public JobProfileDto JobProfile { get; set; }
        public int CourseId { get; set; }
    }
}
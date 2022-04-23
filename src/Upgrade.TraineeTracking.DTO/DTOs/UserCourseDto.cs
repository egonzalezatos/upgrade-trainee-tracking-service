namespace Upgrade.TraineeTracking.DTO.DTOs
{
    public class UserCourseDto
    {
        public int UserId { get; set; }

        //public CourseDto Courses { get; set; } TODO: Enable course dto
        public CourseDto CourseDto { get; set; }

        public decimal Percentage { get; set; }
        public bool Completed { get; set; }
    }
}
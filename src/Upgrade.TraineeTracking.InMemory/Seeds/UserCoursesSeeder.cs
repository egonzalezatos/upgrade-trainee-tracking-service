using Microsoft.EntityFrameworkCore;
using Upgrade.TraineeTracking.Domain.Models;

namespace Upgrade.TraineeTracking.InMemory.Seeds
{
    public class UserCoursesSeeder
    {
        private static object[] data =
        {
            new { Id = "1", Percentage = 99.3m, Completed = false, UserId = 1, CourseId = 1 },
            new { Id = "2", Percentage = 100m, Completed = true, UserId = 1, CourseId = 2 },
            new { Id = "3", Percentage = 50.3m, Completed = false, UserId = 1, CourseId = 3 },
            new { Id = "4", Percentage = 52.3m, Completed = false, UserId = 2, CourseId = 1 },
            new { Id = "5", Percentage = 100m, Completed = true, UserId = 2, CourseId = 2 },
            new { Id = "6", Percentage = 100m, Completed = true, UserId = 2, CourseId = 3 },
        };
        
        public static void Seed(ModelBuilder builder)
        {
            //Arrange
            builder.Entity<UserCourses>()
                .HasData(data);
        }
    }
}
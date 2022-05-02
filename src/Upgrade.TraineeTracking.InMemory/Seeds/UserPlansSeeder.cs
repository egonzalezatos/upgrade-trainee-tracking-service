using Microsoft.EntityFrameworkCore;
using Upgrade.TraineeTracking.Domain.Models;

namespace Upgrade.TraineeTracking.InMemory.Seeds
{
    public class UserPlansSeeder
    {
        private static object[] data =
        {
            new { Id = "1", JobProfileId = 1, UserId = 1, CourseId = 1 },
            new { Id = "2", JobProfileId = 1, UserId = 1, CourseId = 2 },
            new { Id = "3", JobProfileId = 1, UserId = 1, CourseId = 3 },
            new { Id = "4", JobProfileId = 2, UserId = 2, CourseId = 1 },
            new { Id = "5", JobProfileId = 2, UserId = 2, CourseId = 2 },
            new { Id = "6", JobProfileId = 2, UserId = 2, CourseId = 3 },
        };
        
        public static void Seed(ModelBuilder builder)
        {
            //Arrange
            builder.Entity<UserPlan>()
                .HasData(data);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using Upgrade.TraineeTracking.Domain.Models;

namespace Upgrade.TraineeTracking.NonRelational.Seeds
{
    public class UserCoursesSeeder
    {
        private static object[] data =
        {
            new UserCourses { Percentage = 99.3m, Completed = false, UserId = 1, CourseId = 1 },
            new UserCourses { Percentage = 100m, Completed = true, UserId = 1, CourseId = 2 },
            new UserCourses { Percentage = 50.3m, Completed = false, UserId = 1, CourseId = 3 },
            new UserCourses { Percentage = 52.3m, Completed = false, UserId = 2, CourseId = 1 },
            new UserCourses { Percentage = 100m, Completed = true, UserId = 2, CourseId = 2 },
            new UserCourses { Percentage = 100m, Completed = true, UserId = 2, CourseId = 3 },
        };
        
        public static void Seed(IMongoCollection<UserCourses> collection)
        {
            foreach (UserCourses userCourses in data)
            {
                if (!collection.Find(c => c.UserId == userCourses.UserId && c.CourseId == userCourses.CourseId).Any())
                    collection.InsertOneAsync(userCourses);
            }
        }
    }
}
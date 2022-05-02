using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Upgrade.TraineeTracking.Domain.Models;
using Upgrade.TraineeTracking.Domain.Repositories;

namespace Upgrade.TraineeTracking.InMemory.Repositories
{
    public class UserCoursesRepository : Repository<UserCourses, string>, IUserCoursesRepository
    {

        public UserCoursesRepository(DbContext context) : base(context)
        {
        }
        
        public async Task<List<UserCourses>> FindByUserId(int userId)
        {
            var results = await DbSet.Where(d => d.UserId == userId).ToListAsync();
            return results;
        }

        public async Task<List<UserCourses>> GetByUserIdAndProfileId(int userId, int profileId)
        {
            IQueryable<UserCourses> query = Context.Set<UserPlan>()
                .Where(plans => plans.UserId == userId && plans.JobProfileId == profileId)
                .Join(DbSet, plans => plans.UserId, courses => courses.UserId,
                    (plans, courses) => courses);
            
            Console.Out.WriteLine(query.ToQueryString());
            return await query.ToListAsync();
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Upgrade.TraineeTracking.Domain.Models;

namespace Upgrade.TraineeTracking.Domain.Repositories
{
    public interface IUserCoursesRepository : IRepository<UserCourses>
    {
        public Task<List<UserCourses>> FindByUserId(int userId);
        public Task<List<UserCourses>> GetByUserIdAndProfileId(int userId, int profileId);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Upgrade.TraineeTracking.DTO.DTOs;

namespace Upgrade.TraineeTracking.Services.Abstractions.Services
{
    public interface ICourseTrackingService : IService
    {
        public Task<List<UserCourseDto>> Find();
        public Task<List<UserCourseDto>> FindByUserId(int userId);
        public Task<List<UserPlanDto>> FindPlanByUserIdAndProfileId(int userId, int profileId);

        public Task<List<UserCourseDto>> FindByUserPlan(
            int userId, int positionId, int levelId, int technologyId);
    }
}
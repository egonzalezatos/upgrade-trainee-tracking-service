using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Upgrade.TraineeTracking.Services.Abstractions.Services;

namespace Upgrade.TraineeTracking.Api.Controllers
{

    [ApiController]
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseTrackingService _courseTrackingService;

        public CoursesController(ICourseTrackingService courseTrackingService)
        {
            _courseTrackingService = courseTrackingService;
        }

        [HttpGet("/user/{userId}/position/{positionId}/level/{levelId}/technology/{technologyId}")]
        public async Task<IActionResult> GetByUserPlan(int userId, int positionId, int levelId, int technologyId)
        {
            return Ok(await _courseTrackingService.FindByUserPlan(userId, positionId, levelId, technologyId));
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserCoursesByUserId(int userId)
        {
            return Ok(await _courseTrackingService.FindByUserId(userId));
        }
        
        [HttpGet("plan/user/{userId}/profile/{profileId}")]
        public async Task<IActionResult> GetPlanByIdAndProfileId(int userId, int profileId)
        {
            return Ok(await _courseTrackingService.FindPlanByUserIdAndProfileId(userId, profileId));
        }

        [HttpGet("user/{userId}/position/{positionId}/technology/{technologyId}/level/{levelId}")]
        public async Task<IActionResult> GetUserCourses(int userId, int positionId, int technologyId, int levelId)
        {
            return Ok(await _courseTrackingService.FindByUserPlan(
                userId, positionId, technologyId, levelId)
            );
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upgrade.TraineeTracking.Domain.Models;
using Upgrade.TraineeTracking.Domain.Repositories;
using Upgrade.TraineeTracking.DTO.DTOs;
using Upgrade.TraineeTracking.Grpc.Abstractions.Clients;
using Upgrade.TraineeTracking.Services.Abstractions.Mappers;
using Upgrade.TraineeTracking.Services.Abstractions.Services;

namespace Upgrade.TraineeTracking.Services.Services
{
    public class CourseTrackingService : ICourseTrackingService
    {
        private readonly IUserCoursesRepository _trackingRepository;
        private readonly IUserPlanRepository _userPlanRepository;
        private readonly IProfileManagementClient _profileManagementClient;
        private readonly IUserCourseMapper _userCourseMapper;
        private readonly ICoursesServiceClient _coursesServiceClient;

        public CourseTrackingService(IUserCoursesRepository trackingRepository, IUserCourseMapper userCourseMapper, IUserPlanRepository userPlanRepository, IProfileManagementClient profileManagementClient, ICoursesServiceClient coursesServiceClient)
        {
            _trackingRepository = trackingRepository;
            _userCourseMapper = userCourseMapper;
            _userPlanRepository = userPlanRepository;
            _profileManagementClient = profileManagementClient;
            _coursesServiceClient = coursesServiceClient;
        }

        public async Task<List<UserCourseDto>> Find()
        {
            List<UserCourses> docs = await _trackingRepository.GetAsync();
            return _userCourseMapper.ToDto<UserCourseDto>(docs);
        }

        
        
        
        
        public async Task<List<UserCourseDto>> FindByUserId(int userId)
        {
            List<UserCourses> docs = await _trackingRepository.FindByUserId(userId);
            
            List<int> coursesIds = docs.Select(doc => doc.CourseId).Distinct().ToList();
            List<CourseDto> courseDtos = await _coursesServiceClient.GetByIds(coursesIds);
            List<UserCourseDto> dtos = new List<UserCourseDto>();
            foreach (var doc in docs)
            {
                dtos.Add(new UserCourseDto
                {
                    UserId = doc.UserId, 
                    CourseDto = courseDtos.First(c => c.Id == doc.CourseId),
                    Completed = doc.Completed,
                    Percentage = doc.Percentage
                });
            }    
            return dtos;
        }

        public async Task<List<UserPlanDto>> FindPlanByUserIdAndProfileId(int userId, int profileId)
        {
            List<UserPlanDto> planDtos = new List<UserPlanDto>();
            List<UserPlan> docs = await _userPlanRepository.FindByUserIdAndProfileId(userId, profileId);
            List<JobProfileDto> profileDtos = await _profileManagementClient.GetProfilesByUsersIds(new List<int> {userId});
            for (int i = 0; i < docs.Count; i++)
            {
                UserPlan plan = docs[i];
                planDtos.Add(new UserPlanDto
                {
                    UserId = plan.UserId, 
                    JobProfile = profileDtos.First(e => plan.JobProfileId==e.Id), 
                    CourseId = plan.CourseId
                });
            }
            return planDtos;
        }

        public async Task<List<UserCourseDto>> FindByUserPlan(
            int userId, int positionId, int levelId, int technologyId)
        {
            JobProfileDto profile = await _profileManagementClient.GetProfileByFlattenPlan(positionId, levelId, technologyId);
            List<UserCourses> userCourses = await _trackingRepository.GetByUserIdAndProfileId(userId, profile.Id);
            List<int> ids = userCourses.Select(course => course.CourseId).ToList();
            var courseDtosTask =  _coursesServiceClient.GetByIds(ids);
            List<UserCourseDto> userCourseDtos = _userCourseMapper.ToDto<UserCourseDto>(userCourses);
            List<CourseDto> courseDtos = await courseDtosTask;
            foreach (var (dto, entity) in userCourseDtos.Zip(userCourses))
            {
                dto.CourseDto = courseDtos.First(c => c.Id == entity.CourseId);
            }
            return userCourseDtos;
        }
    }
}
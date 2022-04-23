using AutoMapper;
using Upgrade.TraineeTracking.Domain.Models;
using Upgrade.TraineeTracking.DTO.DTOs;

namespace Upgrade.TraineeTracking.Services.MapperProfiles
{
    public class UserCourseProfile : Profile
    {
        public UserCourseProfile()
        {
            CreateMap<UserCourses, UserCourseDto>();

            AllowNullDestinationValues = true;
        }
    }
}
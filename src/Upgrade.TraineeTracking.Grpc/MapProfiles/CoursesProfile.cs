using Upgrade.TraineeTracking.DTO.DTOs;

namespace Upgrade.TraineeTracking.Grpc.MapProfiles
{
    public class CoursesProfile : AutoMapper.Profile
    {
        public CoursesProfile()
        {
            CreateMap<CourseDto, Course>().ReverseMap();
        }
    }
}
using Upgrade.TraineeTracking.DTO.DTOs;

namespace Upgrade.TraineeTracking.Grpc.MapProfiles
{
    public class ProfileProfile : AutoMapper.Profile
    {
        public ProfileProfile()
        {
            CreateMap<Profile, JobProfileDto>()
                .ReverseMap();
            CreateMap<Technology, TechnologyDto>().ReverseMap();
            CreateMap<JobPosition, JobPositionDto>().ReverseMap();
            CreateMap<Level, LevelDto>().ReverseMap();
        }
    }
}
using Upgrade.TraineeTracking.DTO.DTOs;

namespace Upgrade.TraineeTracking.Grpc.MapProfiles
{
    public class ProfileProfile : AutoMapper.Profile
    {
        public ProfileProfile()
        {
            CreateMap<Profile, JobProfileDto>()
                .ReverseMap();
        }
    }
}
using System.Collections.Generic;
using AutoMapper;
using Upgrade.TraineeTracking.Domain.Models;
using Upgrade.TraineeTracking.Services.Abstractions.Mappers;

namespace Upgrade.TraineeTracking.Services.Mappers
{

    public class UserCourseMapper : DocumentMapper<UserCourses>, IUserCourseMapper
    {
        public UserCourseMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}
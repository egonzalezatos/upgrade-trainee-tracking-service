using System.Collections.Generic;
using System.Threading.Tasks;
using Upgrade.TraineeTracking.DTO.DTOs;

namespace Upgrade.TraineeTracking.Grpc.Abstractions.Clients
{
    public interface ICoursesServiceClient
    {
        Task<List<CourseDto>> GetByIds(List<int> Ids);
    }
}
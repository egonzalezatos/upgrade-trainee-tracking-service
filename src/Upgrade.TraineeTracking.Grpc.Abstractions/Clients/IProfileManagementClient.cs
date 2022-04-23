using System.Collections.Generic;
using System.Threading.Tasks;
using Upgrade.TraineeTracking.DTO.DTOs;

namespace Upgrade.TraineeTracking.Grpc.Abstractions.Clients
{
    public interface IProfileManagementClient
    {
        Task<List<JobProfileDto>> GetProfilesByUsersIds(List<int> userIds);
        Task<JobProfileDto> GetProfileByFlattenPlan(int positionId, int levelId, int technologyId);
    }
}
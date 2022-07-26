using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Upgrade.TraineeTracking.DTO.DTOs;
using Upgrade.TraineeTracking.Grpc.Abstractions.Clients;
using Upgrade.TraineeTracking.Grpc.Configurations;
using Upgrade.TraineeTracking.Grpc.Extensions;
using Upgrade.TraineeTracking.Options.grpc;

namespace Upgrade.TraineeTracking.Grpc.Clients
{
    public class ProfileManagementClient : IProfileManagementClient
    {
        private readonly IMapper _mapper;
        private readonly GrpcProfileManagement.GrpcProfileManagementClient _client;

        public ProfileManagementClient(IMapper mapper, GrpcProfileManagement.GrpcProfileManagementClient client)
        {
            _mapper = mapper;
            // Channel = GrpcChannel.ForAddress(_configuration.GetGrpc(GrpcCodeNames.GRPC_PROFILE_MANAGEMENT));
            // _client = new GrpcProfileManagement.GrpcProfileManagementClient(Channel);
            _client = client;
        }

        public virtual async Task<List<JobProfileDto>> GetProfilesByUsersIds(List<int> userIds)
        {
            try
            {
                GetProfilesByUsersIdsRequest request = new GetProfilesByUsersIdsRequest();
                request.UserIds.AddRange(userIds);
                
                //Add token
                
                ProfilesResponse reply = await _client.GetProfilesByUsersIdsAsync(request);
                return _mapper.Map<List<JobProfileDto>>(reply.Profiles.ToList());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public virtual async Task<JobProfileDto> GetProfileByFlattenPlan(int positionId, int levelId, int technologyId)
        {
            try
            {
                var request = new GetProfileByFlattenPlanRequest();
                request.PositionId = positionId;
                request.LevelId = levelId;
                request.TechnologyId = technologyId;

                var response = await _client.GetProfileByFlattenPlanAsync(request);
                return _mapper.Map<JobProfileDto>(response.Profile);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
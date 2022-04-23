using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Upgrade.TraineeTracking.DTO.DTOs;
using Upgrade.TraineeTracking.Grpc.Abstractions.Clients;
using Upgrade.TraineeTracking.Grpc.Configurations;
using Upgrade.TraineeTracking.Grpc.Extensions;

namespace Upgrade.TraineeTracking.Grpc.Clients
{
    public class ProfileManagementClient : IProfileManagementClient
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        protected readonly GrpcChannel Channel;
        private readonly GrpcProfileManagement.GrpcProfileManagementClient _client;

        public ProfileManagementClient(IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _configuration = configuration;
            Channel = GrpcChannel.ForAddress(_configuration.GetGrpc(GrpcCodeNames.GRPC_PROFILE_MANAGEMENT));
            _client = new GrpcProfileManagement.GrpcProfileManagementClient(Channel);
        }

        public virtual async Task<List<JobProfileDto>> GetProfilesByUsersIds(List<int> userIds)
        {
            try
            {
                Console.Out.WriteLine("Calling Profile Management Grpc Service..." + " " + _configuration.GetGrpc(GrpcCodeNames.GRPC_PROFILE_MANAGEMENT));
                GetProfilesByUsersIdsRequest request = new GetProfilesByUsersIdsRequest();
                request.UserIds.AddRange(userIds);
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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Upgrade.TraineeTracking.DTO.DTOs;
using Upgrade.TraineeTracking.Grpc.Configurations;
using Upgrade.TraineeTracking.Shared.RedisUtils;

namespace Upgrade.TraineeTracking.Grpc.Clients
{
    public class ProfileManagementClientCacheable : ProfileManagementClient
    {
        private readonly IDistributedCache _cache;
        public ProfileManagementClientCacheable(IMapper mapper, IConfiguration configuration, IDistributedCache cache) : base(mapper, configuration)
        {
            _cache = cache;
        }

        public override async Task<List<JobProfileDto>> GetProfilesByUsersIds(List<int> ids)
        {
            string cacheKey = RedisKeyBuilder.Key(
                Channel.Target, GrpcCodeNames.GRPC_PROFILE_MANAGEMENT, 
                "GetProfilesByUsersIds", string.Join(",", ids.ToArray()));
            
            byte[] value = await _cache.GetAsync(cacheKey);
            
            if (value is not null) 
                return ByteConverter.Byte2Array<List<JobProfileDto>>(value);
            
            List<JobProfileDto> list = await base.GetProfilesByUsersIds(ids);
            Console.Out.WriteLine($"Updating cache for {cacheKey}");
            await _cache.SetAsync(cacheKey, ByteConverter.Array2Byte(list));
            return list;
        }

        public override async Task<JobProfileDto> GetProfileByFlattenPlan(int positionId, int levelId, int technologyId)
        {
            string cacheKey = RedisKeyBuilder.Key(
                Channel.Target, GrpcCodeNames.GRPC_PROFILE_MANAGEMENT, 
                "GetProfilesByUsersIds", string.Join(",", positionId, levelId, technologyId));
            
            byte[] value = await _cache.GetAsync(cacheKey);
            
            if (value is not null) 
                return ByteConverter.Byte2Array<JobProfileDto>(value);
            
            JobProfileDto list = await base.GetProfileByFlattenPlan(positionId, levelId, technologyId);
            Console.Out.WriteLine($"Updating cache for {cacheKey}");
            await _cache.SetAsync(cacheKey, ByteConverter.Array2Byte(list));
            return list;
        }
    }
}
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
    public class CoursesClientCacheable : CoursesServiceClient
    {
        private readonly IDistributedCache _cache;

        public CoursesClientCacheable(IMapper mapper, GrpcCourses.GrpcCoursesClient client, IDistributedCache cache) : base(mapper, client)
        {
            _cache = cache;
        }

        public override async Task<List<CourseDto>> GetByIds(List<int> ids)
        {
            string cacheKey = RedisKeyBuilder.Key(
                GrpcCodeNames.GRPC_COURSES, 
                "GetByIds", string.Join(",", ids.ToArray()));
            
            byte[] value = await _cache.GetAsync(cacheKey);
            
            if (value is not null) 
                return ByteConverter.Byte2Array<List<CourseDto>>(value);
            
            List<CourseDto> list = await base.GetByIds(ids);
            Console.Out.WriteLine($"Updating cache for {cacheKey}");
            await _cache.SetAsync(cacheKey, ByteConverter.Array2Byte(list));
            return list;
        }
    }
}
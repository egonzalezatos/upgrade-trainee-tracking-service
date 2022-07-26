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
    public class CoursesServiceClient : ICoursesServiceClient
    {
        private readonly IMapper _mapper;
        private readonly GrpcCourses.GrpcCoursesClient _client;

        public CoursesServiceClient(IMapper mapper, GrpcCourses.GrpcCoursesClient client)
        {
            _mapper = mapper;
            _client = client;
        }

        public virtual async Task<List<CourseDto>> GetByIds(List<int> ids)
        {
            GetByIdsRequest request = new GetByIdsRequest();
            request.Ids.AddRange(ids);
            try
            {
                GetByIdsResponse response = await _client.GetByIdsAsync(request);
                return _mapper.Map<List<Course>, List<CourseDto>>(response.Courses.ToList());
            }
            catch (Exception e)
            {
                Console.WriteLine("No se pudo realizar la peticion por gRPC");
                throw;
            }
        }
    }
}
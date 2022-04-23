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
        protected readonly GrpcChannel Channel;
        protected readonly GrpcCourses.GrpcCoursesClient Client;

        public CoursesServiceClient(IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            Channel = GrpcChannel.ForAddress(configuration.GetGrpc(GrpcCodeNames.GRPC_COURSES));
            Client = new GrpcCourses.GrpcCoursesClient(Channel);
        }

        public virtual async Task<List<CourseDto>> GetByIds(List<int> ids)
        {
            GetByIdsRequest request = new GetByIdsRequest();
            request.Ids.AddRange(ids);
            try
            {
                GetByIdsResponse response = await Client.GetByIdsAsync(request);
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
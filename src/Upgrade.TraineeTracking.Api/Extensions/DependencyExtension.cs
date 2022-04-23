using Microsoft.Extensions.DependencyInjection;

namespace Upgrade.TraineeTracking.Api.Extensions
{
    public static class DependencyExtension
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        } 
    }
}
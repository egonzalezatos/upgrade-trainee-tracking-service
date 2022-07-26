using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Upgrade.TraineeTracking.Security.Services;
using Upgrade.TraineeTracking.Security.Services.Abstractions;

namespace Upgrade.TraineeTracking.Security.Extensions
{
    public static class SecurityDependenciesExtension
    {
        public static IServiceCollection AddSecurityDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            //Token provider
            services.AddScoped<ITokenProvider, TokenProvider>();
            if (Convert.ToBoolean(configuration["REDIS_ENABLED"]))
                services.AddScoped<ITokenProvider, CacheableTokenProvider>();
            return services;
        }
        

        
    }
}
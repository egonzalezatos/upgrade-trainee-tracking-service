using System;
using IdentityModel.Client;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Upgrade.TraineeTracking.OIDC.Extensions
{
    public static class DependencyExtension
    {
        public static IServiceCollection AddOIDC(this IServiceCollection services)
        {
            services.AddAccessTokenManagement(options =>
            {
                options.Client.Clients.Add("identityserver", new ClientCredentialsTokenRequest
                {
                    Address = "http://localhost:4777/connect/token",
                    ClientId = "trainee_tracking_service",
                    ClientSecret = "ClientSecret1",
                    Scope = "trainee-admin-service.read" // optional
                });
            })
                .ConfigureBackchannelHttpClient()
                .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(3)
                }));   
            return services;
        }
    }
}
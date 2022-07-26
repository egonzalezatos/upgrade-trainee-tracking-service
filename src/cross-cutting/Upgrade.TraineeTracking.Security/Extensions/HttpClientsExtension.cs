using System;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;
using Upgrade.TraineeTracking.Options.ExternalApis;

namespace Upgrade.TraineeTracking.Security.Extensions
{
    public static class HttpClientsExtension
    {
        public static IServiceCollection AddSecuredHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOidc(configuration);
            services.AddClientAccessTokenHttpClient("HTTP_PMS", 
                    configureClient: cfg => cfg.BaseAddress = new Uri(configuration["Http:Clients:HTTP_PMS"]))
                .AddClientAccessTokenHandler();
            return services;
        }
        
        public static IServiceCollection AddOidc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAccessTokenManagement(options =>
                {
                    options.Client.Clients.Add("identityserver", new ClientCredentialsTokenRequest
                    {
                        Address = $"{configuration["IAM_ADDRESS"]}/connect/token",
                        ClientId = configuration["ClientId"],
                        ClientSecret = configuration["ClientSecret"],
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
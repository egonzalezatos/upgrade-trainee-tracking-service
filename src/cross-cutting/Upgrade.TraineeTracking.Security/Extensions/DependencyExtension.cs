using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Upgrade.TraineeTracking.OIDC.Extensions;

namespace Upgrade.TraineeTracking.Security.Extensions
{
    public static class DependencyExtension
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            Console.WriteLine($"Identity Server Address: {configuration["IAM_Address"]}");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    // base-address of your identity server
                    options.Authority = configuration["IAM_Address"];
                    options.RequireHttpsMetadata = false;


                    // if you are using API resources, you can specify the name here
                    options.Audience = "trainee-tracking-service";

                    // IdentityServer emits a typ header by default, recommended extra check
                    options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                });
            
            services.AddCors(
                options =>
                {
                    options.AddPolicy("mypolicy", builder => builder.WithOrigins("http://localhost:8081"));
                    options.AddPolicy("frontpolicy", builder => builder
                        .WithOrigins("http://localhost")
                        .AllowAnyHeader());
                });

            //TODO: Remove
            services.AddOIDC();
            services.AddClientAccessTokenHttpClient("api", 
                configureClient: cfg => cfg.BaseAddress = new Uri("http://localhost:5002/"))
                .AddClientAccessTokenHandler();
            
            return services;
        }   
    }
}
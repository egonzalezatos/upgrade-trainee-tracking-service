using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Upgrade.TraineeTracking.Api.Extensions
{
    public static class DependencyExtension
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        } 
        
        public static IServiceCollection AddApiSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                // using System.Reflection;
                //var xmlFilename = $"{typeof(DependencyExtension).Assembly.GetName().Name}.xml";
                //c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TraineeTracking", Version = "v1" });
            });
            return services;
        }

        public static IApplicationBuilder UseApiSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "CleanArchProject v1");
            });
            return app;
        }
    }
}
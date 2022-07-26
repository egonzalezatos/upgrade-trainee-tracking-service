using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Upgrade.TraineeTracking.Api.Extensions;
using Upgrade.TraineeTracking.Infrastructure.Extensions;
using Upgrade.TraineeTracking.IoC.Extensions;
using Upgrade.TraineeTracking.Options.Extensions;
using Upgrade.TraineeTracking.Redis.Extensions;
using Upgrade.TraineeTracking.Security.Extensions;

namespace Upgrade.TraineeTracking
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Replace ConnectionStrings from appsettings using Environment Variables 
            services.ReadConfigurationEnvironments(Configuration);
            
            services
                .LoadOptions(Configuration)
                .AddApi()
                .AddInfrastructure(Configuration)
                .AddSecurity(Configuration)
                .AddDependencies(Configuration);
                
            if (Convert.ToBoolean(Configuration["REDIS_ENABLED"]))
                services.AddRedis(Configuration.GetConnectionString("Redis"), "redis");

            services.AddApiSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("frontpolicy");
            app.UseDeveloperExceptionPage();
            
            app.UseApiSwagger();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
    
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            //Seed database
            app.UseSeeds(Configuration);
        }
    }
}

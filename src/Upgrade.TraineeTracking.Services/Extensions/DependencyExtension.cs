using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using Upgrade.TraineeTracking.Services.Abstractions.Mappers;
using Upgrade.TraineeTracking.Services.Abstractions.Services;

namespace Upgrade.TraineeTracking.Services.Extensions
{
    public static class DependencyExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddServices()
                .AddMappers();
            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.Scan(selector => selector
                .FromAssembliesOf(typeof(IService), typeof(DependencyExtension))
                .AddClasses(classes => classes.Where( type => !type.IsInterface && type.Name.EndsWith("Service")))
                .UsingRegistrationStrategy(RegistrationStrategy.Replace(ReplacementBehavior.ServiceType))
                .AsMatchingInterface()
                .WithScopedLifetime());
            return services;
        }

        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(IDocumentMapper), typeof(DependencyExtension));
            services.Scan(selector => selector
                .FromAssembliesOf(typeof(IDocumentMapper), typeof(DependencyExtension))
                .AddClasses(classes => classes.Where( type => !type.IsInterface && type.Name.EndsWith("Mapper")))
                .UsingRegistrationStrategy(RegistrationStrategy.Replace(ReplacementBehavior.ServiceType))
                .AsMatchingInterface()
                .WithScopedLifetime());
            
            return services;
        }
    }
}
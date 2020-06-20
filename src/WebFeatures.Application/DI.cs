using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Application.Interfaces.Requests;
using WebFeatures.Application.Middlewares;
using WebFeatures.Common.Extensions;

namespace WebFeatures.Application
{
    public static class DI
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            AddRequests(services);
            AddValidators(services);
            AddMappings(services);
        }

        private static void AddRequests(IServiceCollection services)
        {
            // Common pipeline
            services.AddScoped(typeof(IRequestMiddleware<,>), typeof(LoggingMiddleware<,>));
            services.AddScoped(typeof(IRequestMiddleware<,>), typeof(PerformanceMiddleware<,>));
            services.AddScoped(typeof(IRequestMiddleware<,>), typeof(ValidationMiddleware<,>));

            // Command pipeline
            services.AddScoped(typeof(IRequestMiddleware<,>), typeof(SaveChangesMiddleware<,>));

            // Endpoints
            services.RegisterTypesFromAssembly(Assembly.GetExecutingAssembly())
                .Where(x => x.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
                .As(x => x.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
                .WithLifetime(ServiceLifetime.Scoped);
        }

        private static void AddValidators(IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Scoped);
        }

        private static void AddMappings(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(x => x.AddProfile<MappingsProfile>());

            mapperConfig.AssertConfigurationIsValid();

            services.AddSingleton(x => mapperConfig.CreateMapper());
        }
    }
}
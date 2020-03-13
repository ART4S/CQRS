using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Application.Middlewares;
using WebFeatures.Requests;

namespace WebFeatures.Application
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            AddRequests(services);
            AddValidators(services);
            AddMappings(services);
        }

        private static void AddRequests(IServiceCollection services)
        {
            services.AddRequests(Assembly.GetExecutingAssembly());

            services.AddScoped(typeof(IRequestMiddleware<,>), typeof(LoggingMiddleware<,>));
            services.AddScoped(typeof(IRequestMiddleware<,>), typeof(ModelValidationMiddleware<,>));
            services.AddScoped(typeof(IRequestMiddleware<,>), typeof(PerformanceMiddleware<,>));
        }

        private static void AddValidators(IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetCallingAssembly(), ServiceLifetime.Scoped);
        }

        private static void AddMappings(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(x => x.AddProfile<MappingsProfile>());
            mapperConfig.AssertConfigurationIsValid();

            services.AddSingleton(x => mapperConfig.CreateMapper());
        }
    }
}

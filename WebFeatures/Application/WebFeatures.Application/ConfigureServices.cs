using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Application.Middlewares;
using WebFeatures.Events;
using WebFeatures.Requests;

namespace WebFeatures.Application
{
    public static class ConfigureServices
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            AddRequests(services);
            AddEvents(services);
            AddValidators(services);
            AddMappings(services);
            AddHangfireJobs(services);
        }

        private static void AddRequests(IServiceCollection services)
        {
            services.AddRequests(Assembly.GetExecutingAssembly());

            // Common pipeline
            services.AddScoped(typeof(IRequestMiddleware<,>), typeof(LoggingMiddleware<,>));
            services.AddScoped(typeof(IRequestMiddleware<,>), typeof(PerformanceMiddleware<,>));
            services.AddScoped(typeof(IRequestMiddleware<,>), typeof(ValidationMiddleware<,>));

            // Commands pipeline
            services.AddScoped(typeof(IRequestMiddleware<,>), typeof(SaveChangesMiddleware<,>));

            // Queries pipeline
            services.AddScoped(typeof(IRequestMiddleware<,>), typeof(QueryFilteringMiddleware<,>));
        }

        private static void AddEvents(IServiceCollection services)
        {
            services.AddEvents(Assembly.GetExecutingAssembly());
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

        public static void AddHangfireJobs(IServiceCollection services)
        {
            Assembly.GetExecutingAssembly();
        }
    }
}

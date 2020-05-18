using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Application.Interfaces.Jobs;
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
            AddJobs(services);
        }

        private static void AddRequests(IServiceCollection services)
        {
            services.AddRequestMediator();

            // Common pipeline
            services.AddScoped(typeof(IRequestMiddleware<,>), typeof(LoggingMiddleware<,>));
            services.AddScoped(typeof(IRequestMiddleware<,>), typeof(PerformanceMiddleware<,>));
            services.AddScoped(typeof(IRequestMiddleware<,>), typeof(ValidationMiddleware<,>));

            // Command pipeline
            services.AddScoped(typeof(IRequestMiddleware<,>), typeof(SaveChangesMiddleware<,>));

            // Endpoints
            Type[] assemblyTypes = Assembly.GetExecutingAssembly().GetTypes();

            foreach (Type type in assemblyTypes)
            {
                Type interfaceType = type.GetInterfaces()
                    .FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));

                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, type);
                }
            }
        }

        private static void AddEvents(IServiceCollection services)
        {
            services.AddEventMediator();

            Type[] assemblyTypes = Assembly.GetExecutingAssembly().GetTypes();

            foreach (Type type in assemblyTypes)
            {
                Type interfaceType = type.GetInterfaces()
                    .FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEventHandler<>));

                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, type);
                }
            }
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

        private static void AddJobs(IServiceCollection services)
        {
            Type[] assemblyTypes = Assembly.GetExecutingAssembly().GetTypes();

            foreach (Type type in assemblyTypes)
            {
                if (type.IsAbstract || type.IsGenericType)
                    continue;

                Type ibackgroundJob = type.GetInterfaces()
                    .FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IBackgroundJob<>));

                if (ibackgroundJob != null)
                {
                    services.AddScoped(ibackgroundJob, type);
                }
            }
        }
    }
}

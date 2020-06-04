using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Middlewares;

namespace WebFeatures.Application
{
    public static class ConfigureServices
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
            services.AddScoped(typeof(IRequestMiddleware<,>), typeof(TransactionMiddleware<,>));
            services.AddScoped(typeof(IRequestMiddleware<,>), typeof(EventsMiddleware<,>));

            // Endpoints
            services.AddTypesFromAsseblyWithInterface(Assembly.GetExecutingAssembly(), typeof(IRequestHandler<,>));
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

    internal static class ConfigureServicesHelper
    {
        public static void AddTypesFromAsseblyWithInterface(this IServiceCollection services,
            Assembly assembly,
            Type interfaceType,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            if (!interfaceType.IsInterface)
            {
                throw new ArgumentException($"{interfaceType} should be an interface type");
            }

            foreach (Type type in assembly.GetTypes())
            {
                IEnumerable<Type> serviceTypes;

                if (interfaceType.IsGenericType)
                {
                    serviceTypes = type.GetInterfaces()
                        .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == interfaceType);
                }
                else
                {
                    serviceTypes = type.GetInterfaces()
                        .Where(x => x == interfaceType);
                }

                foreach (Type serviceType in serviceTypes)
                {
                    services.Add(new ServiceDescriptor(serviceType, type, lifetime));
                }
            }
        }
    }
}
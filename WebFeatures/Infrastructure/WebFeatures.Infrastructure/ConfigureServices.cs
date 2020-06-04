﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebFeatures.Application.Features.ProductComments.Events;
using WebFeatures.Application.Features.Products.Events;
using WebFeatures.Application.Infrastructure.Events;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Application.Interfaces.DataAccess.Reading;
using WebFeatures.Application.Interfaces.Logging;
using WebFeatures.Application.Interfaces.Security;
using WebFeatures.Application.Interfaces.Services;
using WebFeatures.Common;
using WebFeatures.Infrastructure.Common;
using WebFeatures.Infrastructure.DataAccess.Contexts;
using WebFeatures.Infrastructure.DataAccess.Factories;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.EventHandlers;
using WebFeatures.Infrastructure.Events;
using WebFeatures.Infrastructure.Events.Handlers;
using WebFeatures.Infrastructure.Logging;
using WebFeatures.Infrastructure.Requests;
using WebFeatures.Infrastructure.Security;
using WebFeatures.Infrastructure.Services;
using WebFeatures.Persistence;

namespace WebFeatures.Infrastructure
{
    public static class ConfigureServices
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            AddCommon(services);
            AddLogging(services);
            AddDataAccess(services, configuration);
            AddSecurity(services);
            AddEvents(services);
            AddRequests(services);
            AddOtherServices(services);
        }

        private static void AddCommon(IServiceCollection services)
        {
            services.AddScoped<IDateTime, MachineDateTime>();
        }

        private static void AddLogging(IServiceCollection services)
        {
            services.AddScoped(typeof(ILogger<>), typeof(LoggerFacade<>));
        }

        private static void AddDataAccess(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("PostgreSql");

            services.AddSingleton<IDbConnectionFactory>(x => new PostgreSqlDbConnectionFactory(connectionString));
            services.AddSingleton<IEntityProfile>(x =>
            {
                var profile = new EntityProfile();
                profile.RegisterMappingsFromAssembly(Assembly.GetExecutingAssembly());

                return profile;
            });
            services.AddScoped<IWriteDbContext, WriteDbContext>();

            services.AddScoped<IReadDbContext, ReadDbContext>();
        }

        private static void AddSecurity(IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher, PasswordHasher>();
        }

        private static void AddEvents(IServiceCollection services)
        {
            services.AddScoped<IEventStorage, EventStorage>();

            services.AddScoped<IEventHandler<ProductCreated>, ProductCreatedEventHandler>();
            services.AddScoped<IEventHandler<ProductCommentCreated>, ProductCommentCreatedEventHandler>();
        }

        private static void AddRequests(IServiceCollection services)
        {
            services.AddScoped<IRequestMediator, RequestMediator>();
        }

        private static void AddOtherServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
        }

        private static void AddMailing(IServiceCollection services, IConfiguration configuration)
        {
        }

        private static void AddJobs(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddSingleton<IBackgroundJobManager, BackgroundJobManager>();

            //ConfigureServicesHelper.AddOptions<HangfireSettings>(services, configuration);

            //string connectionString = configuration.GetConnectionString("Hangfire");

            //services.AddHangfire(config =>
            //{
            //    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            //    .UseSimpleAssemblyNameTypeSerializer()
            //    .UseRecommendedSerializerSettings()
            //    .UsePostgreSqlStorage(connectionString, new PostgreSqlStorageOptions()
            //    {
            //        PrepareSchemaIfNecessary = true,
            //        QueuePollInterval = TimeSpan.FromMilliseconds(1),
            //    });
            //});
        }
    }
}

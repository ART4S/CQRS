using Hangfire;
using Hangfire.Mongo;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using WebFeatures.Application.Interfaces;
using WebFeatures.Common;
using WebFeatures.HangfireJobs;
using WebFeatures.Infrastructure.Common;
using WebFeatures.Infrastructure.HangfireJobs;
using WebFeatures.Infrastructure.Logging;
using WebFeatures.Infrastructure.Mailing;
using WebFeatures.Infrastructure.Security;
using WebFeatures.Infrastructure.Services;
using WebFeatures.ReadContext;
using WebFeatures.WriteContext;

namespace WebFeatures.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            SetupCommon(services);
            SetupReadContext(services, configuration);
            SetupWriteContext(services);
            SetupMailing(services, configuration);
            SetupSecurity(services);
            SetupSecurity(services);
            SetupServices(services);
            SetupLogging(services);
            SetupHangfireJobs(services, configuration);
        }

        private static void SetupCommon(IServiceCollection services)
        {
            services.AddScoped<IDateTime, MachineDateTime>();
        }

        private static void SetupReadContext(IServiceCollection services, IConfiguration configuration)
        {
            AddOptions<MongoDbSettings>(services, configuration);

            services.AddScoped<IReadContext, MongoDbReadContext>();
            services.AddScoped<MongoDbReadContext>();
        }

        private static void SetupWriteContext(IServiceCollection services)
        {
            services.AddScoped<IWriteContext, EFWriteContext>();
            services.AddDbContext<EFWriteContext>(
                options => options.UseInMemoryDatabase("InMemoryDb"));
        }

        private static void SetupMailing(IServiceCollection services, IConfiguration configuration)
        {
            AddOptions<SmtpClientSettings>(services, configuration);
            services.AddScoped<IEmailSender, SmtpEmailSender>();
        }

        private static void SetupSecurity(IServiceCollection services)
        {
            services.AddDataProtection();
            services.AddScoped<IPasswordEncoder, PasswordEncoder>();
        }

        private static void SetupServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IRequestFilterService, RequestFilterService>();
        }

        private static void SetupLogging(IServiceCollection services)
        {
            services.AddScoped(typeof(ILogger<>), typeof(LoggerFacade<>));
        }

        private static void SetupHangfireJobs(IServiceCollection services, IConfiguration configuration)
        {
            AddOptions<HangfireJobsSettings>(services, configuration);

            services.AddHangfireServices((sp, config) =>
            {
                var settings = sp.GetRequiredService<HangfireJobsSettings>();

                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseMongoStorage(settings.ConnectionString, new MongoStorageOptions()
                {
                    QueuePollInterval = TimeSpan.FromMilliseconds(1)
                });
            });
        }

        private static void AddOptions<TOptions>(IServiceCollection services, IConfiguration configuration)
            where TOptions : class, new()
        {
            services.AddOptions<TOptions>()
                .Bind(configuration.GetSection(typeof(TOptions).Name))
                .ValidateDataAnnotations();

            services.AddSingleton(sp => sp.GetService<IOptions<TOptions>>().Value);
        }
    }
}

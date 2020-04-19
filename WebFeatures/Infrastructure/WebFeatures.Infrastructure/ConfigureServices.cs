using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using WebFeatures.Application.Interfaces.DataContext;
using WebFeatures.Application.Interfaces.Jobs;
using WebFeatures.Application.Interfaces.Logging;
using WebFeatures.Application.Interfaces.Mailing;
using WebFeatures.Application.Interfaces.Security;
using WebFeatures.Application.Interfaces.Services;
using WebFeatures.Common;
using WebFeatures.DataContext;
using WebFeatures.Infrastructure.Common;
using WebFeatures.Infrastructure.Jobs;
using WebFeatures.Infrastructure.Logging;
using WebFeatures.Infrastructure.Mailing;
using WebFeatures.Infrastructure.Security;
using WebFeatures.Infrastructure.Services;

namespace WebFeatures.Infrastructure
{
    public static class ConfigureServices
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            AddCommon(services);
            AddDbContext(services, configuration);
            AddMailing(services, configuration);
            AddSecurity(services);
            AddSecurity(services);
            AddServices(services);
            AddLogging(services);
            AddJobs(services, configuration);
        }

        private static void AddCommon(IServiceCollection services)
        {
            services.AddScoped<IDateTime, MachineDateTime>();
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbContext, BaseDbContext>();

            ConfigureServicesHelper.AddOptions<PostrgeSettings>(services, configuration);
            services.AddDbContext<BaseDbContext>((sp, options) =>
            {
                var settings = sp.GetRequiredService<PostrgeSettings>();
                options.UseNpgsql(settings.ConnectionString);
            });
        }

        private static void AddMailing(IServiceCollection services, IConfiguration configuration)
        {
            ConfigureServicesHelper.AddOptions<SmtpClientSettings>(services, configuration);
            services.AddScoped<IEmailSender, SmtpEmailSender>();
        }

        private static void AddSecurity(IServiceCollection services)
        {
            services.AddDataProtection();
            services.AddScoped<IPasswordEncoder, PasswordEncoder>();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IRequestFilterService, RequestFilterService>();
        }

        private static void AddLogging(IServiceCollection services)
        {
            services.AddScoped(typeof(ILogger<>), typeof(LoggerFacade<>));
        }

        private static void AddJobs(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IBackgroundJobManager, BackgroundJobManager>();

            ConfigureServicesHelper.AddOptions<HangfireSettings>(services, configuration);

            services.AddHangfire((sp, config) =>
            {
                var settings = sp.GetRequiredService<HangfireSettings>();

                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(settings.ConnectionString, new PostgreSqlStorageOptions()
                {
                    PrepareSchemaIfNecessary = true,
                    QueuePollInterval = TimeSpan.FromMilliseconds(1),
                });
            });
        }
    }

    internal static class ConfigureServicesHelper
    {
        public static void AddOptions<TOptions>(IServiceCollection services, IConfiguration configuration)
            where TOptions : class, new()
        {
            services.AddOptions<TOptions>()
                .Bind(configuration.GetSection(typeof(TOptions).Name))
                .ValidateDataAnnotations();

            services.AddSingleton(sp => sp.GetService<IOptions<TOptions>>().Value);
        }
    }
}

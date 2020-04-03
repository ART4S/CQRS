using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WebFeatures.Application.Interfaces;
using WebFeatures.Application.Interfaces.DataContext;
using WebFeatures.Common;
using WebFeatures.Infrastructure.Common;
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
        }

        private static void SetupCommon(IServiceCollection services)
        {
            services.AddScoped<IDateTime, MachineDateTime>();
        }

        private static void SetupReadContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<MongoDbSettings>().ValidateDataAnnotations();

            services.Configure<MongoDbSettings>(
                configuration.GetSection(nameof(MongoDbSettings)));
            services.AddSingleton(sp => sp.GetService<IOptions<MongoDbSettings>>().Value);

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
            services.AddOptions();
            services.Configure<SmtpClientSettings>(
                configuration.GetSection(nameof(SmtpClientSettings)));
            services.AddSingleton(sp => sp.GetService<IOptions<SmtpClientSettings>>().Value);

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
    }
}

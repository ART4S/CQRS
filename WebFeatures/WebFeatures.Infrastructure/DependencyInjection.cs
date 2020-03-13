using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebFeatures.Application.Infrastructure.Events;
using WebFeatures.Application.Interfaces;
using WebFeatures.Common;
using WebFeatures.DataContext;
using WebFeatures.Infrastructure.Common;
using WebFeatures.Infrastructure.CurrentUser;
using WebFeatures.Infrastructure.DataAccess;
using WebFeatures.Infrastructure.Events;
using WebFeatures.Infrastructure.Logging;
using WebFeatures.Infrastructure.Messaging;
using WebFeatures.Infrastructure.Security;

namespace WebFeatures.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseAsyncRepository<>));
            services.AddScoped<IEventMediator, EventMediator>();
            services.AddScoped<IDateTime, MachineDateTime>();
            services.AddScoped(typeof(ILogger<>), typeof(LoggerFacade<>));

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddScoped<IEmailSender, SmtpEmailSender>();
            services.AddOptions<SmtpClientOptions>();
            services.Configure<SmtpClientOptions>(configuration.GetSection("SmtpClient"));

            services.AddDataProtection();
            services.AddScoped<IPasswordEncoder, PasswordEncoder>();

            if (environment.IsDevelopment())
            {
                services.AddDbContext<WebFeaturesDbContext>(
                    options => options.UseInMemoryDatabase("DevDb"));
            }

            if (environment.IsProduction())
            {
                //services.AddDbContext<WebFeaturesDbContext>(
                //    options =>
                //    {
                //        options.UseNpgsql(
                //            configuration.GetConnectionString("Postgre"),
                //            sqlOptions =>
                //            {
                //                sqlOptions.EnableRetryOnFailure(
                //                    maxRetryCount: 5,
                //                    maxRetryDelay: TimeSpan.FromSeconds(30),
                //                    errorCodesToAdd: null);
                //            });
                //    });
            }
        }
    }
}

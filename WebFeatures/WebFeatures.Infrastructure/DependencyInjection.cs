using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebFeatures.Application.Interfaces;
using WebFeatures.Common;
using WebFeatures.DataContext;
using WebFeatures.Infrastructure.Common;
using WebFeatures.Infrastructure.Logging;
using WebFeatures.Infrastructure.Messaging;
using WebFeatures.Infrastructure.Security;
using WebFeatures.Infrastructure.Services;

namespace WebFeatures.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            services.AddScoped<IDateTime, MachineDateTime>();
            services.AddScoped(typeof(ILogger<>), typeof(LoggerFacade<>));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IRequestFilterService, RequestFilterService>();

            services.AddScoped<IEmailSender, SmtpEmailSender>();
            services.AddOptions<SmtpClientOptions>();
            services.Configure<SmtpClientOptions>(configuration.GetSection("SmtpClient"));

            services.AddDataProtection();
            services.AddScoped<IPasswordEncoder, PasswordEncoder>();

            services.AddScoped<IWebFeaturesDbContext, WebFeaturesDbContext>();

            if (environment.IsDevelopment())
            {
                services.AddDbContext<WebFeaturesDbContext>(
                    options => options.UseInMemoryDatabase("Development"));
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

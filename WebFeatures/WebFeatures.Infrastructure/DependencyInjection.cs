using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebFeatures.Application.Infrastructure.Events;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Application.Interfaces.Logging;
using WebFeatures.Application.Interfaces.Security;
using WebFeatures.Common;
using WebFeatures.DataContext;
using WebFeatures.Identity;
using WebFeatures.Identity.Model;
using WebFeatures.Infrastructure.Common;
using WebFeatures.Infrastructure.DataAccess;
using WebFeatures.Infrastructure.Events;
using WebFeatures.Infrastructure.Logging;
using WebFeatures.Infrastructure.Pipeline;
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
            services.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));
            services.AddScoped<IMediator, Mediator>();
            services.AddScoped<IEventBus, EventBus>();
            services.AddScoped<IDateTime, MachineDateTime>();
            services.AddScoped(typeof(ILogger<>), typeof(LoggerFacade<>));
            services.AddScoped<IEmailSender, SmtpEmailSender>();

            services.AddOptions<SmtpClientOptions>();
            services.Configure<SmtpClientOptions>(configuration.GetSection("EmailClient"));

            if (environment.IsDevelopment())
            {
                services.AddDbContext<WebFeaturesDbContext>(
                    options => options.UseInMemoryDatabase("DevDb"));

                services.AddDbContext<ApplicationDbContext>(
                    options => options.UseInMemoryDatabase("DevDb"));

                services.AddIdentity<ApplicationUser, ApplicationRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();
            }

            if (environment.IsProduction())
            {
                //services.AddIdentity<ApplicationUser, ApplicationRole>(
                //        options =>
                //        {
                //            options.User.RequireUniqueEmail = true;

                //            options.Lockout.MaxFailedAccessAttempts = 10;
                //            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);

                //            options.SignIn.RequireConfirmedEmail = true;
                //        })
                //    .AddEntityFrameworkStores<ApplicationDbContext>()
                //    .AddDefaultTokenProviders();
            }
        }
    }
}

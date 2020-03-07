using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebFeatures.Application.Infrastructure.Events;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Interfaces;
using WebFeatures.Common;
using WebFeatures.DataContext;
using WebFeatures.Identity;
using WebFeatures.Identity.Model;
using WebFeatures.Infrastructure.Common;
using WebFeatures.Infrastructure.DataAccess;
using WebFeatures.Infrastructure.Events;
using WebFeatures.Infrastructure.Logging;
using WebFeatures.Infrastructure.Messaging;
using WebFeatures.Infrastructure.Pipeline;

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
            services.AddScoped<IMediator, Mediator>();
            services.AddScoped<IEventMediator, EventMediator>();
            services.AddScoped<IDateTime, MachineDateTime>();
            services.AddScoped(typeof(ILogger<>), typeof(LoggerFacade<>));

            services.AddScoped<IEmailSender, SmtpEmailSender>();
            services.AddOptions<SmtpClientOptions>();
            services.Configure<SmtpClientOptions>(configuration.GetSection("SmtpClient"));

            if (environment.IsDevelopment())
            {
                services.AddDbContext<WebFeaturesDbContext>(
                    options => options.UseInMemoryDatabase("DevDb"));

                services.AddDbContext<ApplicationDbContext>(
                    options => options.UseInMemoryDatabase("DevDb"));

                services.AddIdentity<ApplicationUser, ApplicationRole>(
                        options =>
                        {
                            options.SignIn.RequireConfirmedAccount = false;
                            options.SignIn.RequireConfirmedEmail = false;
                            options.SignIn.RequireConfirmedPhoneNumber = false;

                            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.Zero;

                            options.Password.RequiredLength = 5;
                            options.Password.RequireDigit = false;
                            options.Password.RequireLowercase = false;
                            options.Password.RequireNonAlphanumeric = false;
                            options.Password.RequireUppercase = false;

                            options.User.RequireUniqueEmail = true;
                        })
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();
            }

            if (environment.IsProduction())
            {
                //services.AddDbContext<WebFeaturesDbContext>(
                //    options =>
                //    {
                //        options.UseSqlServer(
                //            configuration.GetConnectionString("Sql"), 
                //            sqlOptions =>
                //            {
                //                sqlOptions.EnableRetryOnFailure(
                //                    maxRetryCount: 5,
                //                    maxRetryDelay: TimeSpan.FromSeconds(30), 
                //                    errorNumbersToAdd: null);
                //            });
                //    });

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

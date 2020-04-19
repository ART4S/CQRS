using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebFeatures.Application.Interfaces.Security;
using WebFeatures.Common;
using WebFeatures.DataContext;
using WebFeatures.Infrastructure.Jobs;

namespace WebFeatures.Infrastructure
{
    public static class Configure
    {
        public static void UseJobsServer(this IApplicationBuilder app)
        {
            app.UseHangfireServer();

            var settings = app.ApplicationServices.GetService<HangfireSettings>();

            if (settings.EnableDashboard)
            {
                app.UseHangfireDashboard("/jobs", new DashboardOptions()
                {
                    //Authorization = new[] { new DashboardAuthorizationFilter() },
                    //IsReadOnlyFunc = ctx => true,
                    DisplayStorageConnectionString = true,
                });
            }
        }

        public static void ConfigurePersictence(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            BaseDbContext context = scope.ServiceProvider.GetService<BaseDbContext>();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            IPasswordEncoder encoder = scope.ServiceProvider.GetService<IPasswordEncoder>();
            IDateTime dateTime = scope.ServiceProvider.GetService<IDateTime>();

            DataSeeder.SeedSampleData(context, encoder, dateTime);
        }
    }
}

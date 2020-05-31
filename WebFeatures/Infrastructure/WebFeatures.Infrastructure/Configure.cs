using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
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
    }
}

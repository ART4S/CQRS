using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebFeatures.JobsServer.Configuration;

namespace WebFeatures.JobsServer.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseJobsServer(this IApplicationBuilder app)
        {
            app.UseHangfireServer();

            var settings = app.ApplicationServices.GetService<Settings>();

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

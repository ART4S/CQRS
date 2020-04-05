using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using WebFeatures.Infrastructure.HangfireJobs;

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
                    Authorization = new[] { new DashboardAuthorizationFilter() },
                    IsReadOnlyFunc = ctx => true,
                    AppPath = "/jobs"
                });
            }
        }
    }

    internal class DashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true;
            HttpContext httpContext = context.GetHttpContext();

            if (httpContext != null)
            {
                ClaimsPrincipal user = httpContext.User;
                if (user != null)
                {
                    return user.Identity.IsAuthenticated;
                }
            }

            return false;
        }
    }
}

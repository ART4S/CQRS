using Hangfire.Dashboard;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace WebFeatures.Infrastructure.Jobs
{
    internal class DashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
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

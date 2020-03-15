using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using WebFeatures.Application.Interfaces;

namespace WebFeatures.Infrastructure.Services
{
    internal class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor contexAccessor)
        {
            var context = contexAccessor.HttpContext;
            if (context?.User?.Identity != null && context.User.Identity.IsAuthenticated)
            {
                UserId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                Roles = context.User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value);
                IsAuthenticated = true;
            }
        }

        public bool IsAuthenticated { get; }
        public string UserId { get; }
        public IEnumerable<string> Roles { get; }
    }
}

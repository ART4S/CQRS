using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using WebFeatures.Application.Interfaces.Services;

namespace WebFeatures.Infrastructure.Services
{
    internal class CurrentUserService : ICurrentUserService
    {
        public Guid UserId { get; }
        public bool IsAuthenticated { get; }

        public CurrentUserService(IHttpContextAccessor contexAccessor)
        {
            HttpContext context = contexAccessor.HttpContext;

            if (context?.User?.Identity != null && context.User.Identity.IsAuthenticated)
            {
                UserId = Guid.Parse(context.User.FindFirstValue(ClaimTypes.NameIdentifier));

                IsAuthenticated = true;
            }
        }
    }
}
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using WebFeatures.Application.Interfaces.Services;

namespace WebFeatures.Infrastructure.Services
{
	internal class CurrentUserService : ICurrentUserService
	{
		public CurrentUserService(IHttpContextAccessor contextAccessor)
		{
			HttpContext context = contextAccessor.HttpContext;

			if (context?.User?.Identity == null || !context.User.Identity.IsAuthenticated) return;

			UserId = Guid.Parse(context.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);

			IsAuthenticated = true;
		}

		public Guid UserId { get; }
		public bool IsAuthenticated { get; }
	}
}

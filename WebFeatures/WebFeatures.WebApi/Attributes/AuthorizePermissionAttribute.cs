using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Permissions.Requests;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Domian.Enums;
using WebFeatures.WebApi.Exceptions;

namespace WebFeatures.WebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    internal class AuthorizePermissionAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly Permission _permission;

        public AuthorizePermissionAttribute(Permission permission)
        {
            _permission = permission;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            IRequestMediator mediator = context.HttpContext.RequestServices.GetRequiredService<IRequestMediator>();

            bool userHasPermission = await mediator.SendAsync(new UserHasPermission() { Permission = _permission });

            if (!userHasPermission)
            {
                throw new AccessDeniedException();
            }
        }
    }
}
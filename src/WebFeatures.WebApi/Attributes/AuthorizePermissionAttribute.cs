using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Authorization.UserHasPermission;
using WebFeatures.Application.Interfaces.Requests;
using WebFeatures.WebApi.Exceptions;

namespace WebFeatures.WebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    internal class AuthorizePermissionAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly string _permission;

        public AuthorizePermissionAttribute(string permission)
        {
            _permission = permission;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            IRequestMediator mediator = context.HttpContext.RequestServices.GetRequiredService<IRequestMediator>();

            bool userHasPermission = await mediator.SendAsync(new UserHasPermissionQuery() { Permission = _permission });

            if (!userHasPermission)
            {
                throw new AccessDeniedException();
            }
        }
    }
}
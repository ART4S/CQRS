using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Auth;
using WebFeatures.Application.Features.Auth.Login;
using WebFeatures.Application.Features.Auth.RegisterUser;
using WebFeatures.Common;
using WebFeatures.WebApi.Controllers.Base;

namespace WebFeatures.WebApi.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IDateTime _dateTime;

        public AuthController(IDateTime dateTime)
        {
            _dateTime = dateTime;
        }

        [HttpPost("[action]")]
        public async Task RegisterUser([FromBody, Required] RegisterUser command)
        {
            UserInfoDto user = await Mediator.SendAsync(command);

            await SignInUser(user);
        }

        [HttpPost("[action]")]
        public async Task Login([FromBody, Required] Login command)
        {
            UserInfoDto user = await Mediator.SendAsync(command);

            await SignInUser(user);
        }

        private Task SignInUser(UserInfoDto user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            var roleClaims = user.Roles.Select(x => new Claim(ClaimTypes.Role, x));
            claims.AddRange(roleClaims);

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var authProperties = new AuthenticationProperties()
            {
                IsPersistent = true,
                ExpiresUtc = _dateTime.Now.AddMinutes(20)
            };

            return HttpContext.SignInAsync(principal, authProperties);
        }

        [Authorize]
        [HttpPost("[action]")]
        public Task Logout()
        {
            return HttpContext.SignOutAsync();
        }
    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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

        [HttpPost("[action]"), ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterCustomer([FromBody, Required] RegisterCustomerCommand command)
        {
            await Mediator.SendCommandAsync(command);
            return Ok();
        }

        [HttpPost("[action]"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody, Required] LoginCommand command)
        {
            var user = await Mediator.SendCommandAsync(command);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };
            claims.AddRange(user.Roles.Select(x => new Claim(ClaimTypes.Role, x)));

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(
                    new ClaimsIdentity(claims)), 
                    new AuthenticationProperties()
                    {
                        IsPersistent = true,
                        ExpiresUtc = _dateTime.Now + TimeSpan.FromMinutes(20)
                    });

            return Ok();
        }

        [HttpPost("[action]"), Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}

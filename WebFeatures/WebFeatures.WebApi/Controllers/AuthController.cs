﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> RegisterUser([FromBody, Required] RegisterUser command)
        {
            var user = await Mediator.SendAsync(command);

            await LoginUser(user);

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody, Required] Login command)
        {
            var user = await Mediator.SendAsync(command);

            await LoginUser(user);

            return Ok();
        }

        private Task LoginUser(UserInfoDto user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };
            claims.AddRange(user.Roles.Select(x => new Claim(ClaimTypes.Role, x)));

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties()
            {
                IsPersistent = true,
                ExpiresUtc = _dateTime.Now.AddMinutes(20)
            };

            return HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        [HttpPost("[action]"), Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}

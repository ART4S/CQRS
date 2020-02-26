using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using WebFeatures.Application.Interfaces.Data;
using WebFeatures.Domian.Entities.Model;
using WebFeatures.WebApi.Controllers.Base;
using WebFeatures.WebApi.ViewModels;

namespace WebFeatures.WebApi.Controllers
{
    /// <summary>
    /// Аутентификация
    /// </summary>
    public class AuthController : BaseController
    {
        public AuthController(IAuthService authService)
        {
            _userRepo = userRepo;
        }

        /// <summary>
        /// Войти в систему
        /// </summary>
        [HttpPost("[action]")]
        public IActionResult Login([FromBody, Required] LoginViewModel loginVm)
        {
            var claims = Mediator.SendCommand(command);

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20)
                }).Wait();

            return Ok();
        }

        /// <summary>
        /// Выйти из системы
        /// </summary>
        [HttpPost("[action]")]     
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Accounts.Requests.Commands;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Common.SystemTime;
using WebFeatures.WebApi.Controllers.Base;

namespace WebFeatures.WebApi.Controllers
{
    /// <summary>
    /// Аутентификация
    /// </summary>
    public class AccountsController : BaseController
    {
        /// <summary>
        /// Зарегистрировать нового пользователя
        /// </summary>
        /// <response code="200">Успех</response>
        /// <response code="400" cref="ValidationError">Неверные пользовательские данные</response>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
        public async Task Register([FromBody, Required] Register request)
        {
            Guid userId = await Mediator.SendAsync(request);

            await SignInUser(userId);
        }

        /// <summary>
        /// Войти на сайт
        /// </summary>
        /// <response code="200">Успех</response>
        /// <response code="400" cref="ValidationError">Неверные пользовательские данные</response>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
        public async Task Login([FromBody, Required] Login request)
        {
            Guid userId = await Mediator.SendAsync(request);

            await SignInUser(userId);
        }

        private Task SignInUser(Guid userId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var authProperties = new AuthenticationProperties()
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeProvider.DateTime.Now.AddMinutes(20)
            };

            return HttpContext.SignInAsync(principal, authProperties);
        }

        /// <summary>
        /// Выйти из сайта
        /// </summary>
        /// <response code="200">Успех</response>
        [HttpPost("[action]")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task Logout()
        {
            return HttpContext.SignOutAsync();
        }
    }
}
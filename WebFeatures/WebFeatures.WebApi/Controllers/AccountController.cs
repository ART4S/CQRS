using Microsoft.AspNetCore.Antiforgery;
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
using WebFeatures.Application.Features.Auth.Dto;
using WebFeatures.Application.Features.Auth.Requests.Commands;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Common;
using WebFeatures.WebApi.Controllers.Base;

namespace WebFeatures.WebApi.Controllers
{
    /// <summary>
    /// Аутентификация
    /// </summary>
    public class AccountController : BaseController
    {
        private readonly IDateTime _dateTime;
        private readonly IAntiforgery _antiforgery;

        public AccountController(IDateTime dateTime, IAntiforgery antiforgery)
        {
            _dateTime = dateTime;
            _antiforgery = antiforgery;
        }

        /// <summary>
        /// Зарегистрировать нового пользователя
        /// </summary>
        /// <response code="200">Успех</response>
        /// <response code="400" cref="ValidationError">Неверные пользовательские данные</response>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
        public async Task Register([FromBody, Required] RegisterUser request)
        {
            UserInfoDto user = await Mediator.SendAsync(request);

            await SignInUser(user);

            _antiforgery.GetAndStoreTokens(HttpContext);
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
            UserInfoDto user = await Mediator.SendAsync(request);

            await SignInUser(user);

            _antiforgery.GetAndStoreTokens(HttpContext);
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

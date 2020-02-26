using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebFeatures.Application.Features.Registration.RegisterUser;
using WebFeatures.WebApi.Controllers.Base;

namespace WebFeatures.WebApi.Controllers
{
    /// <summary>
    /// Регистрация новых пользователей
    /// </summary>
    public class RegistrationController : BaseController
    {
        /// <summary>
        /// Зарегистрировать нового пользователя
        /// </summary>
        [HttpPost("[action]")]
        public IActionResult RegisterUser([FromBody, Required] RegisterUserCommand command)
        {
            Mediator.SendCommand(command);
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mime;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;

namespace WebFeatures.WebApi.Controllers.Base
{
    /// <summary>
    /// Базовый класс для контроллеров
    /// </summary>
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// Посредник для отправки запроса подходящим обработчикам
        /// </summary>
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        private IMediator _mediator;
    }
}

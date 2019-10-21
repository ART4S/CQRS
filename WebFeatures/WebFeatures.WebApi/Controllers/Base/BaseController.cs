﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mime;
using WebFeatures.Application.Infrastructure.Pipeline.Mediators;

namespace WebFeatures.WebApi.Controllers.Base
{
    /// <summary>
    /// Базовый класс для контроллеров
    /// </summary>
    [Route("api/v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Authorize]
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Посредник для отправки запроса подходящим обработчикам
        /// </summary>
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        private IMediator _mediator;
    }
}

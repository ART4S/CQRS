using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mime;
using WebFeatures.Application.Interfaces.Requests;

namespace WebFeatures.WebApi.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public abstract class BaseController : ControllerBase
    {
        protected IRequestMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IRequestMediator>();
        private IRequestMediator _mediator;

        protected IActionResult Created(object value)
        {
            return StatusCode(StatusCodes.Status201Created, value);
        }
    }
}
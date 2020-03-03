using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mime;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;

namespace WebFeatures.WebApi.Controllers.Base
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        private IMediator _mediator;
    }
}

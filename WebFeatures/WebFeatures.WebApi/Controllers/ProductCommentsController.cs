using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebFeatures.Application.Features.ProductComments.Requests.Commands;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.WebApi.Controllers.Base;

namespace WebFeatures.WebApi.Controllers
{
    /// <summary>
    /// Комментарии к товарам
    /// </summary>
    public class ProductCommentsController : BaseController
    {
        /// <summary>
        /// Создать комментарий
        /// </summary>
        /// <returns>Идентификатор созданного комментария</returns>
        /// <response code="200" cref="Guid">Успех</response>
        /// <response code="400" cref="ValidationError">Ошибка валидации</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
        public Task<Guid> CreateComment([FromForm, Required] CreateProductComment request)
            => Mediator.SendAsync(request);
    }
}
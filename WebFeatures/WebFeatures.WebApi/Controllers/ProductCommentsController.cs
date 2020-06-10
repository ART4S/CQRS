using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebFeatures.Application.Features.ProductComments.Requests.Commands;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Domian.Enums;
using WebFeatures.WebApi.Attributes;
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
        /// <response code="403">Доступ запрещен</response>
        [HttpPost]
        [Authorize]
        [AuthorizePermission(Permission.CreateProductComment)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public Task<Guid> Create([FromForm, Required] CreateProductComment request)
            => Mediator.SendAsync(request);
    }
}
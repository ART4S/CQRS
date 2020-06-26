using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebFeatures.Application.Constants;
using WebFeatures.Application.Features.ProductComments.Requests.Commands;
using WebFeatures.Application.Infrastructure.Results;
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
        /// <response code="201" cref="Guid">Успех</response>
        /// <response code="400" cref="ValidationError">Ошибка валидации</response>
        /// <response code="403">Доступ запрещен</response>
        [HttpPost]
        [Authorize]
        [AuthorizePermission(PermissionConstants.ProductComments.Create)]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Create([FromForm, Required] CreateProductComment request)
        {
            return Created(await Mediator.SendAsync(request));
        }
    }
}
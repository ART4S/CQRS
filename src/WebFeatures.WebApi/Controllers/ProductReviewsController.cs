using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebFeatures.Application.Constants;
using WebFeatures.Application.Features.ProductReviews.Requests.Commands;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.WebApi.Attributes;
using WebFeatures.WebApi.Controllers.Base;

namespace WebFeatures.WebApi.Controllers
{
    /// <summary>
    /// Обзоры на товары
    /// </summary>
    public class ProductReviewsController : BaseController
    {
        /// <summary>
        /// Создать обзор
        /// </summary>
        /// <returns>Идентификатор созданного обзора</returns>
        /// <response code="201">Успех</response>
        /// <response code="400" cref="ValidationError">Ошибка валидации</response>
        /// <response code="403">Доступ запрещен</response>
        [HttpPost]
        [Authorize]
        [AuthorizePermission(PermissionConstants.ProductReviews.Create)]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Create([FromForm, Required] CreateProductReview request)
        {
            return Created(await Mediator.SendAsync(request));
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebFeatures.Application.Features.ProductReviews.Requests.Commands;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Domian.Enums;
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
        /// <response code="200">Успех</response>
        /// <response code="400" cref="ValidationError">Ошибка валидации</response>
        /// <response code="403">Доступ запрещен</response>
        [HttpPost]
        [Authorize]
        [AuthorizePermission(Permission.CreateProductReview)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public Task Create([FromForm, Required] CreateProductReview request)
            => Mediator.SendAsync(request);
    }
}
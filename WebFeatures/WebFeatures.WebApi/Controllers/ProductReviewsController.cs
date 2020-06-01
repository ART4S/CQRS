using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebFeatures.Application.Features.ProductReviews.Requests.Commands;
using WebFeatures.Application.Infrastructure.Results;
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
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
        public Task CreateReview([FromForm, Required] CreateProductReview request)
            => Mediator.SendAsync(request);
    }
}
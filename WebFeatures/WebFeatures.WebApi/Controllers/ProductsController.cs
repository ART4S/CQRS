using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.Dto;
using WebFeatures.Application.Features.Products.Requests.Commands;
using WebFeatures.Application.Features.Products.Requests.Queries;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Domian.Enums;
using WebFeatures.WebApi.Attributes;
using WebFeatures.WebApi.Controllers.Base;

namespace WebFeatures.WebApi.Controllers
{
    /// <summary>
    /// Товары
    /// </summary>
    public class ProductsController : BaseController
    {
        /// <summary>
        /// Получить товар
        /// </summary>
        /// <param name="id">Идентификатор товара</param>
        /// <returns>Товар</returns>
        /// <response code="200" cref="ProductInfoDto">Успех</response>
        /// <response code="400" cref="ValidationError">Товар отсутствует</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ProductInfoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
        public Task<ProductInfoDto> Get(Guid id)
            => Mediator.SendAsync(new GetProduct() { Id = id });

        /// <summary>
        /// Получить список товаров
        /// </summary>
        /// <returns>Список</returns>
        /// <response code="200" cref="IEnumerable{ProductInfoDto}">Успех</response>
        [HttpGet("list")]
        [ProducesResponseType(typeof(IEnumerable<ProductListDto>), StatusCodes.Status200OK)]
        public Task<IEnumerable<ProductListDto>> GetList()
            => Mediator.SendAsync(new GetProductList());

        /// <summary>
        /// Получить обзоры на товар
        /// </summary>
        /// <param name="id">Идентификатор товара</param>
        /// <returns>Обзоры</returns>
        /// <response code="200" cref="IEnumerable{ProductReviewInfoDto}">Успех</response>
        [HttpGet("{id:guid}/reviews")]
        [ProducesResponseType(typeof(IEnumerable<ProductReviewInfoDto>), StatusCodes.Status200OK)]
        public Task<IEnumerable<ProductReviewInfoDto>> GetReviews(Guid id)
            => Mediator.SendAsync(new GetProductReviews() { ProductId = id });

        /// <summary>
        /// Получить комментарии к товару
        /// </summary>
        /// <param name="id">Идентификатор товара</param>
        /// <returns>Комментарии</returns>
        /// <response code="200" cref="IEnumerable{ProductCommentInfoDto}">Успех</response>
        [HttpGet("{id:guid}/comments")]
        [ProducesResponseType(typeof(IEnumerable<ProductCommentInfoDto>), StatusCodes.Status200OK)]
        public Task<IEnumerable<ProductCommentInfoDto>> GetComments(Guid id)
            => Mediator.SendAsync(new GetProductComments() { ProductId = id });

        /// <summary>
        /// Создать товар
        /// </summary>
        /// <returns>Идентификатор созданного товара</returns>
        /// <response code="200" cref="Guid">Успех</response>
        /// <response code="400" cref="ValidationError">Ошибка валидации</response>
        /// <response code="403">Доступ запрещен</response>
        [HttpPost]
        [Authorize]
        [AuthorizePermission(Permission.CreateProduct)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public Task<Guid> Create([FromForm, Required] CreateProduct request)
            => Mediator.SendAsync(request);

        /// <summary>
        /// Редактировать товар
        /// </summary>
        /// <response code="200">Успех</response>
        /// <response code="400" cref="ValidationError">Ошибка валидации</response>
        /// <response code="403">Доступ запрещен</response>
        [HttpPut("{id:guid}")]
        [Authorize]
        [AuthorizePermission(Permission.UpdateProduct)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public Task Update(Guid id, [FromForm, Required] UpdateProduct request)
        {
            request.Id = id;

            return Mediator.SendAsync(request);
        }
    }
}
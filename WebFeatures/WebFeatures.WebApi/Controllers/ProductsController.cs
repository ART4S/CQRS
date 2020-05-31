using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.Dto;
using WebFeatures.Application.Features.Products.Requests.Commands;
using WebFeatures.Application.Features.Products.Requests.Queries;
using WebFeatures.WebApi.Controllers.Base;

namespace WebFeatures.WebApi.Controllers
{
    public class ProductsController : BaseController
    {
        [HttpGet("{id:guid}")]
        public Task<ProductInfoDto> Get([FromRoute] Guid id)
            => Mediator.SendAsync(new GetProduct() { Id = id });

        [HttpGet("list")]
        public Task<IEnumerable<ProductListDto>> GetProductsList()
            => Mediator.SendAsync(new GetProductsList());

        [HttpGet("{id:guid}/reviews")]
        public Task<IEnumerable<ProductReviewInfoDto>> GetReviews([FromRoute] Guid id)
            => Mediator.SendAsync(new GetProductReviews() { ProductId = id });

        [HttpGet("{id:guid}/comments")]
        public Task<IEnumerable<ProductCommentInfoDto>> GetComments([FromRoute] Guid id)
            => Mediator.SendAsync(new GetProductComments() { ProductId = id });

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public Task<Guid> CreateProduct([FromForm, Required] CreateProduct request)
            => Mediator.SendAsync(request);

        [HttpPut("{id:guid}")]
        //[ValidateAntiForgeryToken]
        public Task EditProduct([FromForm, Required] EditProduct request)
            => Mediator.SendAsync(request);
    }
}

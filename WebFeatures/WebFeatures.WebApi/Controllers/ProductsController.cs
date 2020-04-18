using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.CreateProduct;
using WebFeatures.Application.Features.Products.EditProduct;
using WebFeatures.Application.Features.Products.GetProduct;
using WebFeatures.Application.Features.Products.GetProductComments;
using WebFeatures.Application.Features.Products.GetProductsList;
using WebFeatures.Application.Features.Products.GetReviews;
using WebFeatures.WebApi.Controllers.Base;

namespace WebFeatures.WebApi.Controllers
{
    public class ProductsController : BaseController
    {
        [HttpGet("{id:guid}")]
        public Task<ProductInfoDto> Get([FromRoute] Guid id)
            => Mediator.SendAsync(new GetProduct() { Id = id });

        [HttpGet("list")]
        public Task<IQueryable<ProductListDto>> GetProductsList()
            => Mediator.SendAsync(new GetProductsList());

        [HttpGet("{id:guid}/reviews")]
        public Task<IQueryable<ReviewInfoDto>> GetReviews([FromRoute] Guid id)
            => Mediator.SendAsync(new GetReviews() { ProductId = id });

        [HttpGet("{id:guid}/comments")]
        public Task<IQueryable<CommentInfoDto>> GetComments([FromRoute] Guid id)
            => Mediator.SendAsync(new GetProductComments() { ProductId = id });

        [HttpPost]
        public Task<Guid> CreateProduct([FromForm, Required] CreateProduct request)
            => Mediator.SendAsync(request);

        [HttpPut("{id:guid}")]
        public Task EditProduct([FromForm, Required] EditProduct request)
            => Mediator.SendAsync(request);
    }
}

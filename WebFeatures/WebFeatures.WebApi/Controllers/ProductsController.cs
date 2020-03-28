using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.CreateProduct;
using WebFeatures.Application.Features.Products.EditProduct;
using WebFeatures.Application.Features.Products.GetProductById;
using WebFeatures.Application.Features.Products.GetProductsList;
using WebFeatures.Application.Features.Products.GetRatingsSummary;
using WebFeatures.Application.Features.Products.GetReviews;
using WebFeatures.WebApi.Controllers.Base;

namespace WebFeatures.WebApi.Controllers
{
    public class ProductsController : BaseController
    {
        [HttpGet("{id:guid}")]
        public Task<ProductInfoDto> Get([FromRoute] Guid id)
            => Mediator.SendAsync(new GetProductById() { Id = id });

        [HttpGet("list")]
        public Task<IQueryable<ProductListDto>> GetProductsList()
            => Mediator.SendAsync(new GetProductsList());

        [HttpGet("{id:guid}/reviews")]
        public Task<IQueryable<ReviewInfoDto>> GetReviews([FromRoute] Guid id)
            => Mediator.SendAsync(new GetReviews() { ProductId = id });

        [HttpGet("{id:guid}/[action]")]
        public Task<RatingsSummaryDto> GetRatingsSummary([FromRoute] Guid id)
            => Mediator.SendAsync(new GetRatingsSummary() { ProductId = id });

        [HttpPost]
        public Task<Guid> CreateProduct([FromForm] CreateProduct request)
            => Mediator.SendAsync(request);

        [HttpPut("{id:guid}")]
        public Task EditProduct([FromForm] EditProduct request)
            => Mediator.SendAsync(request);
    }
}

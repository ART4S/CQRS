using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.CreateProduct;
using WebFeatures.Application.Features.Products.GetProductsList;
using WebFeatures.WebApi.Controllers.Base;

namespace WebFeatures.WebApi.Controllers
{
    public class ProductsController : BaseController
    {
        [HttpGet("list")]
        public async Task<IQueryable<ProductListDto>> GetProductsList()
            => await Mediator.SendAsync(new GetProductsList());

        [HttpPost]
        public async Task<Guid> CreateProduct([FromForm] CreateProduct request)
            => await Mediator.SendAsync(request);
    }
}

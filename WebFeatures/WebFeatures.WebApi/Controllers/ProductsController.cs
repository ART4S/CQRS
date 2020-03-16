using Microsoft.AspNetCore.Http;
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
        [ProducesResponseType(typeof(IQueryable<ProductListDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductsList()
            => Ok(await Mediator.SendAsync(new GetProductsList()));

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateProduct(CreateProduct command)
            => Ok(await Mediator.SendAsync(command));
    }
}

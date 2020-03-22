using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebFeatures.Application.Features.Products.CreateProduct;
using WebFeatures.Application.Features.Products.GetProductsList;
using WebFeatures.WebApi.Controllers.Base;
using WebFeatures.WebApi.Utils;

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
        public async Task<IActionResult> CreateProduct([FromForm] CreateProduct request)
            => Ok(await Mediator.SendAsync(request));
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.GetProductsList;
using WebFeatures.WebApi.Controllers.Base;

namespace WebFeatures.WebApi.Controllers
{

    public class ProductsController : BaseController
    {
        [HttpGet("list")]
        public async Task<IActionResult> GetProductsList()
        {
            return Ok(await Mediator.SendAsync(new GetProductsList()));
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebFeatures.Application.Features.ProductComments.Requests.Commands;
using WebFeatures.WebApi.Controllers.Base;

namespace WebFeatures.WebApi.Controllers
{
    public class ProductCommentsController : BaseController
    {
        [HttpPost]
        public Task<Guid> CreateComment([FromForm, Required] CreateProductComment request)
            => Mediator.SendAsync(request);
    }
}

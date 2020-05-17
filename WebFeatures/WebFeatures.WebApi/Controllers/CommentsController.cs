using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebFeatures.Application.Features.ProductComments.CreateProductComment;
using WebFeatures.WebApi.Controllers.Base;

namespace WebFeatures.WebApi.Controllers
{
    public class CommentsController : BaseController
    {
        [HttpPost]
        public Task<Guid> CreateComment([FromBody, Required] CreateProductComment request)
            => Mediator.SendAsync(request);
    }
}

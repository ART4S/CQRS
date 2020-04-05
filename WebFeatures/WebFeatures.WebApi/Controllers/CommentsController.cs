using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebFeatures.WebApi.Controllers.Base;
using WebFeatures.Application.Features.Comments.CreateComment;

namespace WebFeatures.WebApi.Controllers
{
    public class CommentsController : BaseController
    {
        [HttpPost]
        public Task<Guid> CreateComment(CreateComment request)
            => Mediator.SendAsync(request);
    }
}

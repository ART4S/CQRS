using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Comments.CreateComment;
using WebFeatures.WebApi.Controllers.Base;

namespace WebFeatures.WebApi.Controllers
{
    public class CommentsController : BaseController
    {
        [HttpPost]
        public Task CreateComment(CreateComment request)
            => Mediator.SendAsync(request);
    }
}

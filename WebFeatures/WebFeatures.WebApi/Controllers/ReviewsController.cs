using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Reviews.CreateReview;
using WebFeatures.WebApi.Controllers.Base;

namespace WebFeatures.WebApi.Controllers
{
    public class ReviewsController : BaseController
    {
        [HttpPost]
        public Task<Guid> CreateReview(CreateReview request)
            => Mediator.SendAsync(request);
    }
}

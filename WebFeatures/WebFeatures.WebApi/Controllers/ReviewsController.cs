using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebFeatures.Application.Features.ProductReviews.CreateProductReview;
using WebFeatures.WebApi.Controllers.Base;

namespace WebFeatures.WebApi.Controllers
{
    public class ReviewsController : BaseController
    {
        [HttpPost]
        public Task CreateReview([FromBody, Required] CreateProductReview request)
            => Mediator.SendAsync(request);
    }
}

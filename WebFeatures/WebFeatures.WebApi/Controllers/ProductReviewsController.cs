using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebFeatures.Application.Features.ProductReviews.Requests.Commands;
using WebFeatures.WebApi.Controllers.Base;

namespace WebFeatures.WebApi.Controllers
{
    public class ProductReviewsController : BaseController
    {
        [HttpPost]
        public Task CreateReview([FromForm, Required] CreateProductReview request)
            => Mediator.SendAsync(request);
    }
}

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Exceptions;
using WebFeatures.Application.Interfaces;
using WebFeatures.Common;
using WebFeatures.Domian.Entities;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Reviews.CreateReview
{
    public class CreateReviewHandler : IRequestHandler<CreateReview, Guid>
    {
        private readonly IWebFeaturesDbContext _db;
        private readonly ICurrentUserService _user;
        private readonly IDateTime _dateTime;

        public CreateReviewHandler(
            IWebFeaturesDbContext db,
            ICurrentUserService user,
            IDateTime dateTime)
        {
            _db = db;
            _user = user;
            _dateTime = dateTime;
        }

        public async Task<Guid> HandleAsync(CreateReview request, CancellationToken cancellationToken)
        {
            if (_db.Reviews.Any(
                x => x.UserId == _user.UserId && x.ProductId == request.ProductId))
            {
                throw new ApplicationValidationException("Review of this user already exists");
            }

            var review = new Review(
                _user.UserId,
                request.ProductId,
                request.Title,
                request.Comment,
                _dateTime.Now,
                request.Rating);

            await _db.Reviews.AddAsync(review, cancellationToken);

            return review.Id;
        }
    }
}

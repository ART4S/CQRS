using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Exceptions;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Entities;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Reviews.CreateReview
{
    public class CreateReviewHandler : IRequestHandler<CreateReview, Guid>
    {
        private readonly IWebFeaturesDbContext _db;
        private readonly ICurrentUserService _user;

        public CreateReviewHandler(IWebFeaturesDbContext db, ICurrentUserService user)
            => (_db, _user) = (db, user);

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
                request.Header,
                request.Body);

            await _db.Reviews.AddAsync(review, cancellationToken);

            return review.Id;
        }
    }
}

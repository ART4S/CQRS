using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces;
using WebFeatures.Application.Interfaces.DataContext;
using WebFeatures.Common;
using WebFeatures.Domian.Entities;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Reviews.CreateReview
{
    public class CreateReviewHandler : IRequestHandler<CreateReview, Guid>
    {
        private readonly IWriteContext _db;
        private readonly ICurrentUserService _currentUser;
        private readonly IDateTime _dateTime;

        public CreateReviewHandler(
            IWriteContext db,
            ICurrentUserService currentUser,
            IDateTime dateTime)
        {
            _db = db;
            _currentUser = currentUser;
            _dateTime = dateTime;
        }

        public async Task<Guid> HandleAsync(CreateReview request, CancellationToken cancellationToken)
        {
            var review = new Review(
                _currentUser.UserId,
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

using AutoMapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataContext;
using WebFeatures.Application.Interfaces.Services;
using WebFeatures.Domian.Entities;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Reviews.CreateReview
{
    public class CreateReviewHandler : IRequestHandler<CreateReview, Guid>
    {
        private readonly IWriteContext _db;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public CreateReviewHandler(
            IWriteContext db,
            IMapper mapper,
            ICurrentUserService currentUser)
        {
            _db = db;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<Guid> HandleAsync(CreateReview request, CancellationToken cancellationToken)
        {
            Review review = _mapper.Map<Review>(request);
            review.AuthorId = _currentUser.UserId;

            await _db.Reviews.AddAsync(review, cancellationToken);

            review.Events.Add(new ReviewCreatedEvent(review.Id));

            return review.Id;
        }
    }
}

using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IDateTime _dateTime;

        public CreateReviewHandler(
            IWriteContext db,
            ICurrentUserService currentUser,
            IMapper mapper,
            IDateTime dateTime)
        {
            _db = db;
            _currentUser = currentUser;
            _mapper = mapper;
            _dateTime = dateTime;
        }

        public async Task<Guid> HandleAsync(CreateReview request, CancellationToken cancellationToken)
        {
            Review review = _mapper.Map<Review>(request);

            review.CreatedAt = _dateTime.Now;
            review.AuthorId = _currentUser.UserId;

            await _db.Reviews.AddAsync(review, cancellationToken);

            return review.Id;
        }
    }
}

using AutoMapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.Events;
using WebFeatures.Application.Interfaces.Requests;
using WebFeatures.Application.Interfaces.Services;
using WebFeatures.Domian.Entities.Products;

namespace WebFeatures.Application.Features.ProductReviews.CreateProductReview
{
    internal class CreateProductReviewCommandHandler : IRequestHandler<CreateProductReviewCommand, Guid>
    {
        private readonly IWriteDbContext _db;
        private readonly IEventMediator _events;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public CreateProductReviewCommandHandler(
            IWriteDbContext db,
            IEventMediator events,
            ICurrentUserService currentUser,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _currentUser = currentUser;
            _events = events;
        }

        public async Task<Guid> HandleAsync(CreateProductReviewCommand request, CancellationToken cancellationToken)
        {
            ProductReview review = _mapper.Map<ProductReview>(request);

            review.AuthorId = _currentUser.UserId;

            await _db.ProductReviews.CreateAsync(review);

            await _events.PublishAsync(new ProductReviewCreated(review.Id));

            return review.Id;
        }
    }
}
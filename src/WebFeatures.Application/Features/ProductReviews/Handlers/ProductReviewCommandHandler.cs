using AutoMapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Features.ProductReviews.Events;
using WebFeatures.Application.Features.ProductReviews.Requests.Commands;
using WebFeatures.Application.Infrastructure.Events;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.Services;
using WebFeatures.Domian.Entities.Products;

namespace WebFeatures.Application.Features.ProductReviews.Handlers
{
    internal class ProductReviewCommandHandler : IRequestHandler<CreateProductReview, Guid>
    {
        private readonly IWriteDbContext _db;
        private readonly IEventMediator _events;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public ProductReviewCommandHandler(
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

        public async Task<Guid> HandleAsync(CreateProductReview request, CancellationToken cancellationToken)
        {
            ProductReview review = _mapper.Map<ProductReview>(request);
            review.AuthorId = _currentUser.UserId;

            await _db.ProductReviews.CreateAsync(review);

            await _events.PublishAsync(new ProductReviewCreated(review.Id));

            return review.Id;
        }
    }
}
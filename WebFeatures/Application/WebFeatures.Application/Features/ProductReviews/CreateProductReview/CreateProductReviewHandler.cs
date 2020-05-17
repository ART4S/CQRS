using AutoMapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Application.Interfaces.Services;
using WebFeatures.Domian.Entities;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.ProductReviews.CreateProductReview
{
    internal class CreateProductReviewHandler : IRequestHandler<CreateProductReview, Guid>
    {
        private readonly IDbContext _db;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public CreateProductReviewHandler(
            IDbContext db,
            IMapper mapper,
            ICurrentUserService currentUser)
        {
            _db = db;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<Guid> HandleAsync(CreateProductReview request, CancellationToken cancellationToken)
        {
            ProductReview review = _mapper.Map<ProductReview>(request);
            review.AuthorId = _currentUser.UserId;

            await _db.ProductReviews.CreateAsync(review);

            return review.Id;
        }
    }
}

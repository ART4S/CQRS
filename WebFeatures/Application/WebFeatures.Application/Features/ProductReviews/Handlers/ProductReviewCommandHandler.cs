﻿using AutoMapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Features.ProductReviews.Requests.Commands;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Application.Interfaces.Services;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Features.ProductReviews.Handlers
{
    internal class ProductReviewCommandHandler : IRequestHandler<CreateProductReview, Guid>
    {
        private readonly IWriteDbContext _db;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public ProductReviewCommandHandler(
            IWriteDbContext db,
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
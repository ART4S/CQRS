﻿using Bogus;
using WebFeatures.Application.Features.ProductReviews.CreateProductReview;
using WebFeatures.Domian.Enums;

namespace WebFeatures.Application.Tests.Common.Stubs.Requests.ProductReviews
{
    internal class CreateProductReviewStub : Faker<CreateProductReviewCommand>
    {
        public CreateProductReviewStub()
        {
            StrictMode(true);

            RuleFor(x => x.ProductId, x => x.Random.Guid());
            RuleFor(x => x.Title, x => x.Lorem.Sentence());
            RuleFor(x => x.Comment, x => x.Lorem.Sentences());
            RuleFor(x => x.Rating, x => x.PickRandom<ProductRating>());
        }
    }
}

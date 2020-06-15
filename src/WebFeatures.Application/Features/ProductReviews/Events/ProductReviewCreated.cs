using System;
using WebFeatures.Application.Infrastructure.Events;

namespace WebFeatures.Application.Features.ProductReviews.Events
{
    public class ProductReviewCreated : IEvent
    {
        public Guid Id { get; }

        public ProductReviewCreated(Guid id)
        {
            Id = id;
        }
    }
}

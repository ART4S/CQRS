using System;
using WebFeatures.Events;

namespace WebFeatures.Application.Features.Reviews.CreateReview
{
    public class ReviewCreatedEvent : IEvent
    {
        public Guid ReviewId { get; }
        public ReviewCreatedEvent(Guid reviewId) => ReviewId = reviewId;
    }
}

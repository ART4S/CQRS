using System;
using WebFeatures.Application.Interfaces.Events;

namespace WebFeatures.Application.Features.ProductComments.Events
{
    public class ProductCommentCreated : IEvent
    {
        public Guid Id { get; }

        public ProductCommentCreated(Guid id)
        {
            Id = id;
        }
    }
}

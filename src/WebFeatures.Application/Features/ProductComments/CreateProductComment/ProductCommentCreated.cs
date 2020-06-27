using System;
using WebFeatures.Application.Interfaces.Events;

namespace WebFeatures.Application.Features.ProductComments.CreateProductComment
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

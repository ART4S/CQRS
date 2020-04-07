using System;
using WebFeatures.Events;

namespace WebFeatures.Application.Features.Products.CreateProduct
{
    internal class ProductCreatedEvent : IEvent
    {
        public Guid ProductId { get; }

        public ProductCreatedEvent(Guid productId) => ProductId = productId;
    }
}
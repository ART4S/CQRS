using System;
using WebFeatures.Events;

namespace WebFeatures.Application.Features.Products.EditProduct
{
    internal class ProductEditedEvent : IEvent
    {
        public Guid ProductId { get; }
        public ProductEditedEvent(Guid productId) => ProductId = productId;
    }
}

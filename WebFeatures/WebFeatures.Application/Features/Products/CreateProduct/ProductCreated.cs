using System;
using WebFeatures.Events;

namespace WebFeatures.Application.Features.Products.CreateProduct
{
    public class ProductCreated : IEvent
    {
        public Guid ProductId { get; }
        public ProductCreated(Guid productId) => ProductId = productId;
    }
}

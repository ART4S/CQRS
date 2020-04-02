using System;
using WebFeatures.Events;

namespace WebFeatures.Application.Features.Products.EditProduct
{
    public class ProductEdited : IEvent
    {
        public Guid ProductId { get; }
        public ProductEdited(Guid productId) => ProductId = productId;
    }
}

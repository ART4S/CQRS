using System;
using WebFeatures.Application.Interfaces.Events;

namespace WebFeatures.Application.Features.Products.Events
{
    public class ProductUpdated : IEvent
    {
        public Guid Id { get; }

        public ProductUpdated(Guid id)
        {
            Id = id;
        }
    }
}

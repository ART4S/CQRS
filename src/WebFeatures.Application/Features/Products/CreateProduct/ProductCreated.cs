using System;
using WebFeatures.Application.Interfaces.Events;

namespace WebFeatures.Application.Features.Products.Events
{
    public class ProductCreated : IEvent
    {
        public Guid Id { get; }

        public ProductCreated(Guid id)
        {
            Id = id;
        }
    }
}

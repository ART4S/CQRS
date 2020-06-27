using System;
using WebFeatures.Application.Interfaces.Events;

namespace WebFeatures.Application.Features.Products.Events
{
    public class ProductDeleted : IEvent
    {
        public Guid Id { get; }

        public ProductDeleted(Guid id) => Id = id;
    }
}
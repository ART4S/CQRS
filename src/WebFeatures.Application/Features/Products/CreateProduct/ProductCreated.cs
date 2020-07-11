using System;
using WebFeatures.Application.Interfaces.Events;

namespace WebFeatures.Application.Features.Products.CreateProduct
{
	public class ProductCreated : IEvent
	{
		public ProductCreated(Guid id)
		{
			Id = id;
		}

		public Guid Id { get; }
	}
}

using System;
using WebFeatures.Application.Interfaces.Events;

namespace WebFeatures.Application.Features.Products.UpdateProduct
{
	public class ProductUpdated : IEvent
	{
		public ProductUpdated(Guid id)
		{
			Id = id;
		}

		public Guid Id { get; }
	}
}

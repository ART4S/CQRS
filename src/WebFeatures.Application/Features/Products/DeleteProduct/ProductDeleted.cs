using System;
using WebFeatures.Application.Interfaces.Events;

namespace WebFeatures.Application.Features.Products.DeleteProduct
{
	public class ProductDeleted : IEvent
	{
		public ProductDeleted(Guid id)
		{
			Id = id;
		}

		public Guid Id { get; }
	}
}

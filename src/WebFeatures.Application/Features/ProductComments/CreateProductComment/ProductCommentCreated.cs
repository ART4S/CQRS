using System;
using WebFeatures.Application.Interfaces.Events;

namespace WebFeatures.Application.Features.ProductComments.CreateProductComment
{
	public class ProductCommentCreated : IEvent
	{
		public ProductCommentCreated(Guid id)
		{
			Id = id;
		}

		public Guid Id { get; }
	}
}

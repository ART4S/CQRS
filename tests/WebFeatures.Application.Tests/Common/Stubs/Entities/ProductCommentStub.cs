using System;
using Bogus;
using Bogus.Extensions;
using WebFeatures.Domian.Entities.Products;

namespace WebFeatures.Application.Tests.Common.Stubs.Entities
{
	internal class ProductCommentStub : Faker<ProductComment>
	{
		public ProductCommentStub()
		{
			StrictMode(true);

			RuleForType(typeof(Guid), x => x.Random.Guid());
			RuleForType(typeof(Guid?), x => ((Guid?) x.Random.Guid()).OrDefault(x));

			RuleFor(x => x.Body, x => x.Lorem.Sentences());
			RuleFor(x => x.CreateDate, DateTime.UtcNow);

			Ignore(x => x.Product);
			Ignore(x => x.Author);
			Ignore(x => x.ParentComment);
		}
	}
}

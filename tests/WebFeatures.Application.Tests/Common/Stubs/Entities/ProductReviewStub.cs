using System;
using Bogus;
using WebFeatures.Domian.Entities.Products;
using WebFeatures.Domian.Enums;

namespace WebFeatures.Application.Tests.Common.Stubs.Entities
{
	internal class ProductReviewStub : Faker<ProductReview>
	{
		public ProductReviewStub()
		{
			StrictMode(true);

			RuleForType(typeof(Guid), x => x.Random.Guid());

			RuleFor(x => x.Title, x => x.Lorem.Sentence());
			RuleFor(x => x.Comment, x => x.Lorem.Sentences());
			RuleFor(x => x.CreateDate, DateTime.UtcNow);
			RuleFor(x => x.Rating, x => x.PickRandom<ProductRating>());

			Ignore(x => x.Author);
			Ignore(x => x.Product);
		}
	}
}

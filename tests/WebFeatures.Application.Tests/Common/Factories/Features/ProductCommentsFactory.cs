using Bogus;
using WebFeatures.Application.Features.ProductComments.Requests.Commands;

namespace WebFeatures.Application.Tests.Common.Factories.Features
{
    internal static class ProductCommentsFactory
    {
        public static CreateProductComment CreateProductComment()
        {
            var request = new Faker<CreateProductComment>()
                .RuleFor(x => x.ProductId, x => x.Random.Guid())
                .RuleFor(x => x.Body, x => x.Lorem.Text());

            return request;
        }
    }
}
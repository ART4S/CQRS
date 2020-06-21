using Bogus;
using WebFeatures.Application.Features.ProductComments.Requests.Commands;

namespace WebFeatures.Application.Tests.Common.Stubs.Features.ProductComments
{
    internal class CreateProductCommentStub : Faker<CreateProductComment>
    {
        public CreateProductCommentStub()
        {
            StrictMode(true);

            RuleFor(x => x.ProductId, x => x.Random.Guid());
            RuleFor(x => x.Body, x => x.Lorem.Sentences());
        }
    }
}
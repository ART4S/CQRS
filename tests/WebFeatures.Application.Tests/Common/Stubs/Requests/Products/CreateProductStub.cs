using Bogus;
using Bogus.Extensions;
using Moq;
using System;
using WebFeatures.Application.Features.Products.CreateProduct;
using WebFeatures.Application.Interfaces.Files;

namespace WebFeatures.Application.Tests.Common.Stubs.Requests.Products
{
    internal class CreateProductStub : Faker<CreateProductCommand>
    {
        public CreateProductStub()
        {
            StrictMode(true);

            RuleForType(typeof(Guid), x => x.Random.Guid());
            RuleForType(typeof(Guid?), x => ((Guid?)x.Random.Guid()).OrDefault(x));

            RuleFor(x => x.Name, x => x.Lorem.Word());
            RuleFor(x => x.Price, x => x.Random.Decimal().OrDefault(x));
            RuleFor(x => x.Description, x => x.Lorem.Text().OrNull(x));
            RuleFor(x => x.MainPicture, Mock.Of<IFile>());
            RuleFor(x => x.Pictures, new IFile[0]);
        }
    }
}

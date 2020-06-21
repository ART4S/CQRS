using Bogus;
using Bogus.Extensions;
using System;
using WebFeatures.Domian.Entities.Products;

namespace WebFeatures.Application.Tests.Common.Stubs.Entities
{
    internal class ProductStub : Faker<Product>
    {
        public ProductStub()
        {
            StrictMode(true);

            RuleForType(typeof(Guid), x => x.Random.Guid());
            RuleForType(typeof(Guid?), x => ((Guid?)x.Random.Guid()).OrDefault(x));

            RuleFor(x => x.Name, x => x.Lorem.Word());
            RuleFor(x => x.Price, x => x.Random.Decimal().OrDefault(x));
            RuleFor(x => x.Description, x => x.Lorem.Text().OrNull(x));
            RuleFor(x => x.CreateDate, DateTime.UtcNow);

            Ignore(x => x.MainPicture);
            Ignore(x => x.Manufacturer);
            Ignore(x => x.Category);
            Ignore(x => x.Brand);
        }
    }
}
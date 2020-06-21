using Bogus;
using Bogus.Extensions;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Infrastructure.Tests.Common.Stubs.Entities
{
    internal class ManufacturerStub : Faker<Manufacturer>
    {
        public ManufacturerStub()
        {
            StrictMode(true);

            RuleFor(x => x.Id, x => x.Random.Guid());
            RuleFor(x => x.HomePageUrl, x => x.Internet.Url().OrNull(x));
            RuleFor(x => x.OrganizationName, x => x.Company.CompanyName());
            RuleFor(x => x.StreetAddress, new AddressStub());
        }
    }
}
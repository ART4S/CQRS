using Bogus;
using System;
using WebFeatures.Domian.ValueObjects;

namespace WebFeatures.Infrastructure.Tests.Common.Stubs.Entities
{
    internal class AddressStub : Faker<Address>
    {
        private static readonly Guid[] Cities =
        {
            new Guid("b27a7a05-d61f-4559-9b04-5fd282a694d3"),
            new Guid("b9f5c008-24da-49e2-bc83-b7356920881b")
        };

        public AddressStub()
        {
            StrictMode(true);

            RuleFor(x => x.CityId, x => x.PickRandom(Cities));
            RuleFor(x => x.PostalCode, x => x.Address.ZipCode());
            RuleFor(x => x.StreetName, x => x.Address.StreetName());

            Ignore(x => x.City);
        }
    }
}
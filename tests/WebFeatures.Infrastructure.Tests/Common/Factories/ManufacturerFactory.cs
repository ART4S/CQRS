using Bogus;
using Bogus.Extensions;
using System;
using WebFeatures.Domian.Entities;
using Address = WebFeatures.Domian.ValueObjects.Address;

namespace WebFeatures.Infrastructure.Tests.Common.Factories
{
    internal static class ManufacturerFactory
    {
        public static Manufacturer Get()
        {
            var cities = new[]
            {
                new Guid("b27a7a05-d61f-4559-9b04-5fd282a694d3"),
                new Guid("b9f5c008-24da-49e2-bc83-b7356920881b")
            };

            var address = new Faker<Address>()
                .StrictMode(true)
                .RuleFor(x => x.CityId, x => x.PickRandom(cities))
                .RuleFor(x => x.PostalCode, x => x.Random.Utf16String())
                .RuleFor(x => x.StreetName, x => x.Random.Utf16String())
                .Ignore(x => x.City);

            var manufacturer = new Faker<Manufacturer>()
                .StrictMode(true)
                .RuleFor(x => x.Id, x => x.Random.Guid())
                .RuleFor(x => x.HomePageUrl, x => x.Random.Utf16String().OrNull(x))
                .RuleFor(x => x.OrganizationName, x => x.Random.Utf16String())
                .RuleFor(x => x.StreetAddress, x => address);

            return manufacturer;
        }
    }
}
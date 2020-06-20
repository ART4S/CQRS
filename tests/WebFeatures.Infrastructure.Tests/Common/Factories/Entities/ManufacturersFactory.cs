using Bogus;
using Bogus.Extensions;
using System;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.ValueObjects;

namespace WebFeatures.Infrastructure.Tests.Common.Factories.Entities
{
    internal static class ManufacturersFactory
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
                .RuleFor(x => x.PostalCode, x => x.Address.ZipCode())
                .RuleFor(x => x.StreetName, x => x.Address.StreetName())
                .Ignore(x => x.City);

            var manufacturer = new Faker<Manufacturer>()
                .StrictMode(true)
                .RuleFor(x => x.Id, x => x.Random.Guid())
                .RuleFor(x => x.HomePageUrl, x => x.Internet.Url().OrNull(x))
                .RuleFor(x => x.OrganizationName, x => x.Company.CompanyName())
                .RuleFor(x => x.StreetAddress, x => address);

            return manufacturer;
        }
    }
}
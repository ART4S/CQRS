using System;
using WebFeatures.Domian.Model.Abstractions;

namespace WebFeatures.Domian.Model
{
    public class City : BaseEntity
    {
        public string Name { get; }

        public Guid CountryId { get; }
        public Country Country { get; }

        public City(Country country, string name)
        {
            Name = name;

            CountryId = country.Id;
            Country = country;
        }
    }
}
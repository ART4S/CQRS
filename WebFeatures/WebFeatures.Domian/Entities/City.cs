using System;
using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; }

        public Guid CountryId { get; }
        public Country Country { get; }

        public City(string name, Country country)
        {
            Name = name;

            CountryId = country.Id;
            Country = country;
        }

        private City() { } // For EF
    }
}
using System;
using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; }

        public Guid CountryId { get; }
        public Country Country { get; }

        public City(string name, Guid countryId)
        {
            Name = name;
            CountryId = countryId;
        }

        private City() { } // For EF
    }
}
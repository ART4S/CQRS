using System;
using System.Collections.Generic;
using WebFeatures.Domian.Model.Abstractions;

namespace WebFeatures.Domian.Model.ValueObjects
{
    public class StreetAddress : ValueObject
    {
        public Guid CityId { get; }
        public City City { get; }

        public string Name { get; }

        public StreetAddress(string name, City city)
        {
            Name = name;

            CityId = city.Id;
            City = city;
        }

        private StreetAddress() { } // For EF

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
            yield return CityId;
        }
    }
}
using System;
using System.Collections.Generic;
using WebFeatures.Domian.Model.Abstractions;

namespace WebFeatures.Domian.Model.ValueObjects
{
    public class StreetAddress : ValueObject
    {
        public Guid CityId { get; }
        public City City { get; }

        public string StreetName { get; }

        public StreetAddress(string name, City city)
        {
            StreetName = name;

            CityId = city.Id;
            City = city;
        }

        private StreetAddress() { } // For EF
        protected override IEnumerable<object> GetComparisionValues()
        {
            yield return StreetName;
            yield return CityId;
        }
    }
}
using System;
using System.Collections.Generic;
using WebFeatures.Domian.Common;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Domian.ValueObjects
{
    public class StreetAddress : ValueObject
    {
        public Guid CityId { get; }
        public City City { get; }

        public string StreetName { get; }

        public string PostalCode { get; }

        public StreetAddress(string streetName, string postalCode, City city)
        {
            StreetName = streetName;
            PostalCode = postalCode;

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
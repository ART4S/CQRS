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

        public StreetAddress(string streetName, string postalCode, Guid cityId)
        {
            StreetName = streetName;
            PostalCode = postalCode;
            CityId = cityId;
        }

        private StreetAddress() { } // For EF

        protected override IEnumerable<object> GetComparisionValues()
        {
            yield return StreetName;
            yield return CityId;
            yield return PostalCode;
        }
    }
}
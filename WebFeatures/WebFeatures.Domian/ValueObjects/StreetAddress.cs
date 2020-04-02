using System;
using System.Collections.Generic;
using WebFeatures.Domian.Common;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Domian.ValueObjects
{
    public class StreetAddress : ValueObject
    {
        public string StreetName { get; set; }
        public string PostalCode { get; set; }

        public Guid CityId { get; set; }
        public City City { get; set; }

        protected override IEnumerable<object> GetComparisionValues()
        {
            yield return StreetName;
            yield return CityId;
            yield return PostalCode;
        }
    }
}
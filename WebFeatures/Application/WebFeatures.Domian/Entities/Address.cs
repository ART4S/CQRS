using System;
using WebFeatures.Domian.Common;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Domian.ValueObjects
{
    public class Address : Entity
    {
        public string StreetName { get; set; }
        public string PostalCode { get; set; }

        public Guid CityId { get; set; }
        public City City { get; set; }
    }
}
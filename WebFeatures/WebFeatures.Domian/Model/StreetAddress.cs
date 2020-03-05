using System.Collections.Generic;
using WebFeatures.Domian.Model.Abstractions;

namespace WebFeatures.Domian.Model.OrderAggregate
{
    public class StreetAddress : ValueObject
    {
        public string Country { get; }
        public string Street { get; }
        public string City { get; }

        public StreetAddress(string country, string street, string city)
        {
            Country = country;
            Street = street;
            City = city;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Country;
            yield return Street;
            yield return City;
        }
    }
}
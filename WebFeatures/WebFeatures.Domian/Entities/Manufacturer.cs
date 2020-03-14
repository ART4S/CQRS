using System.Collections.Generic;
using WebFeatures.Domian.Common;
using WebFeatures.Domian.ValueObjects;

namespace WebFeatures.Domian.Entities
{
    public class Manufacturer : BaseEntity
    {
        public string OrganizationName { get; set; }
        public string HomePageUrl { get; set; }
        public StreetAddress StreetAddress { get; }

        public IReadOnlyCollection<Product> Products => _products.AsReadOnly();
        private readonly List<Product> _products = new List<Product>();

        public Manufacturer(string organizationName, StreetAddress address)
        {
            OrganizationName = organizationName;
            StreetAddress = address;
        }

        private Manufacturer() { } // For EF
    }
}
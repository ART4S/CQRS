using System.Collections.Generic;
using WebFeatures.Domian.Attibutes;
using WebFeatures.Domian.Common;
using WebFeatures.Domian.ValueObjects;

namespace WebFeatures.Domian.Entities
{
    [EntityMetadata("Manufacturers")]
    public class Manufacturer : BaseEntity
    {
        public string OrganizationName { get; set; }
        public string HomePageUrl { get; set; }
        public StreetAddress StreetAddress { get; set; }

        public ICollection<Product> Products { get; private set; } = new HashSet<Product>();
    }
}
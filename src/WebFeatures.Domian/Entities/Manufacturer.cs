using WebFeatures.Domian.Common;
using WebFeatures.Domian.ValueObjects;

namespace WebFeatures.Domian.Entities
{
    public class Manufacturer : Entity
    {
        public string OrganizationName { get; set; }
        public string HomePageUrl { get; set; }
        public Address StreetAddress { get; set; }
    }
}
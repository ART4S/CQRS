using WebFeatures.Domian.Common;
using WebFeatures.Domian.ValueObjects;

namespace WebFeatures.Domian.Entities
{
    public class Shipper : Entity
    {
        public string OrganizationName { get; set; }
        public StreetAddress HeadOffice { get; set; }
        public string ContactPhone { get; set; }
    }
}

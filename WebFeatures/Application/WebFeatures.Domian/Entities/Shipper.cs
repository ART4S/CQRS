using WebFeatures.Domian.Common;
using WebFeatures.Domian.ValueObjects;

namespace WebFeatures.Domian.Entities
{
    public class Shipper : IdentityEntity
    {
        public string OrganizationName { get; set; }
        public string ContactPhone { get; set; }
        public Address HeadOffice { get; set; }
    }
}

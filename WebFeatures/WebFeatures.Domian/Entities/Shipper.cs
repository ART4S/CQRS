using WebFeatures.Domian.Common;
using WebFeatures.Domian.ValueObjects;

namespace WebFeatures.Domian.Entities
{
    public class Shipper : BaseEntity
    {
        public string OrganizationName { get; }

        public StreetAddress HeadOffice { get; }

        public string ContactPhone { get; set; }

        public Shipper(string organizationName, StreetAddress headOffice)
        {
            OrganizationName = organizationName;
            HeadOffice = headOffice;
        }

        private Shipper() { } // For EF
    }
}

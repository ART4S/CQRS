using System;
using WebFeatures.Domian.Common;
using WebFeatures.Domian.ValueObjects;

namespace WebFeatures.Domian.Entities
{
    public class Shipper : Entity
    {
        public string OrganizationName { get; set; }
        public string ContactPhone { get; set; }

        public Guid HeadOfficeId { get; set; }
        public Address HeadOffice { get; set; }
    }
}

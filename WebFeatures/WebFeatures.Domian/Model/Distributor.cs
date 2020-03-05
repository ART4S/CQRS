using System.Collections.Generic;
using WebFeatures.Domian.Model.Abstractions;
using WebFeatures.Domian.Model.OrderAggregate;

namespace WebFeatures.Domian.Model
{
    public class Distributor : BaseEntity
    {
        public Distributor()
        {
            ShippingCenters = new HashSet<StreetAddress>();
        }

        public ICollection<StreetAddress> ShippingCenters { get; }
    }
}

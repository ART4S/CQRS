using System;
using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }

        public Guid CountryId { get; set; }
        public Country Country { get; set; }
    }
}
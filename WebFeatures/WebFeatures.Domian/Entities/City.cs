using System;
using WebFeatures.Domian.Attibutes;
using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
{
    [TableName("Cities")]
    public class City : BaseEntity
    {
        public string Name { get; set; }

        public Guid CountryId { get; set; }
        public Country Country { get; set; }
    }
}
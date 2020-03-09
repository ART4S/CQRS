using WebFeatures.Domian.Model.Abstractions;
using WebFeatures.Domian.Model.ValueObjects;

namespace WebFeatures.Domian.Model
{
    public class Manufacturer : BaseEntity
    {
        public string Name { get; set; }
        public StreetAddress StreetAddress { get; }

        public Manufacturer(string name, StreetAddress address)
        {
            Name = name;
            StreetAddress = address;
        }
    }
}
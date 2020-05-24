using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
{
    public class Country : IdentityEntity
    {
        public string Name { get; set; }
        public string Continent { get; set; }
    }
}
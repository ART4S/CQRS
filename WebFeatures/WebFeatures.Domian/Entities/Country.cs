using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
{
    public class Country : BaseEntity
    {
        public string Name { get; }
        public string Continent { get; }

        public Country(string name, string continent)
        {
            Name = name;
            Continent = continent;
        }

        private Country() { } // For EF
    }
}
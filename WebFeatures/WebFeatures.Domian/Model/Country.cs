using WebFeatures.Domian.Model.Abstractions;

namespace WebFeatures.Domian.Model
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
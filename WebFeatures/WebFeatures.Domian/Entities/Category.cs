using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; }

        public Category(string name)
        {
            Name = name;
        }

        public Category() { } // For EF
    }
}

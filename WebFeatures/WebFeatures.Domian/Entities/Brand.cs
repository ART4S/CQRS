using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
{
    public class Brand : BaseEntity
    {
        public string Name { get; }

        public Brand(string name)
        {
            Name = name;
        }

        public Brand() { } // For EF
    }
}

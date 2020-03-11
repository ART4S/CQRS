using WebFeatures.Domian.Model.Abstractions;

namespace WebFeatures.Domian.Model
{
    public class Role : BaseEntity
    {
        public string Name { get; }
        public string Description { get; set; }

        public Role(string name, string description)
        {
            Name = name;
            Description = description;
        }

        private Role() { } // For EF
    }
}

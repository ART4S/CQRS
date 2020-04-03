using System;

namespace WebFeatures.Domian.Attibutes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class EntityMetadataAttribute : Attribute
    {
        public string Name { get; }
        public string Description { get; }

        public EntityMetadataAttribute(string name, string description = null)
        {
            Name = name;
            Description = description;
        }
    }
}

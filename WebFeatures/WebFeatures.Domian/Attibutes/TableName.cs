using System;

namespace WebFeatures.Domian.Attibutes
{
    public class TableNameAttribute : Attribute
    {
        public string Name { get; }

        public TableNameAttribute(string name) => Name = name;
    }
}

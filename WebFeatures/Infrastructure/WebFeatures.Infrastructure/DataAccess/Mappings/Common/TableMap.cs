using WebFeatures.Common;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Common
{
    internal class TableMap
    {
        public string Name { get; }
        public string Schema { get; private set; } = "public";

        public TableMap(string name)
        {
            Guard.ThrowIfNullOrWhiteSpace(name, nameof(name));
            Name = name;
        }

        public void WithSchema(string schema)
        {
            Guard.ThrowIfNullOrWhiteSpace(schema, nameof(schema));
            Schema = schema;
        }
    }
}

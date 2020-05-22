using WebFeatures.Common;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Common
{
    internal partial class TableMap
    {
        public string Name { get; }
        public string Schema { get; private set; } = "public";

        public TableMap(string name)
        {
            Guard.ThrowIfNullOrWhiteSpace(name, nameof(name));

            Name = name;
        }
    }
}

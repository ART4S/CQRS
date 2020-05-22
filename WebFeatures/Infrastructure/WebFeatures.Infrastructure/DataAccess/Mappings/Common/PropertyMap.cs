using WebFeatures.Common;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Common
{
    internal partial class PropertyMap
    {
        public string Property { get; }

        public string Field { get; private set; }

        public PropertyMap(string property)
        {
            Guard.ThrowIfNullOrWhiteSpace(property, nameof(property));

            Property = Field = property;
        }

        public override int GetHashCode()
        {
            return Property.GetHashCode();
        }
    }
}

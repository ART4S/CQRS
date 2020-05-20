using WebFeatures.Common;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Common
{
    internal class PropertyMap
    {
        public string Property { get; }

        public string Field { get; private set; }

        public PropertyMap(string property)
        {
            Guard.ThrowIfNullOrWhiteSpace(property, nameof(property));

            Property = Field = property;
        }

        public void ToField(string field)
        {
            Guard.ThrowIfNullOrWhiteSpace(field, nameof(field));
            Field = field;
        }

        public override int GetHashCode()
        {
            return Property.GetHashCode();
        }
    }
}

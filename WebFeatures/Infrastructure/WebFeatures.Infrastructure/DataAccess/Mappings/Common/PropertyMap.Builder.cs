using WebFeatures.Common;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Common
{
    internal partial class PropertyMap
    {
        public class Builder
        {
            private readonly PropertyMap _property;

            public Builder(PropertyMap property)
            {
                _property = property;
            }

            public void ToField(string field)
            {
                Guard.ThrowIfNullOrWhiteSpace(field, nameof(field));

                _property.Field = field;
            }
        }
    }
}

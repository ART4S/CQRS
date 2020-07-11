using WebFeatures.Common;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Common
{
	internal partial class PropertyMap<TEntity>
		where TEntity : class
	{
		public class Builder
		{
			private readonly PropertyMap<TEntity> _property;

			public Builder(PropertyMap<TEntity> property)
			{
				_property = property;
			}

			public void ToColumn(string column)
			{
				Guard.ThrowIfNullOrEmpty(column, nameof(column));

				_property.ColumnName = column;
			}
		}
	}
}

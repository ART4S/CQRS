using WebFeatures.Common;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Common
{
	internal partial class TableMap
	{
		public class Builder
		{
			private readonly TableMap _table;

			public Builder(TableMap table)
			{
				_table = table;
			}

			public void WithSchema(string schema)
			{
				Guard.ThrowIfNullOrEmpty(schema, nameof(schema));

				_table.Schema = schema;
			}
		}
	}
}

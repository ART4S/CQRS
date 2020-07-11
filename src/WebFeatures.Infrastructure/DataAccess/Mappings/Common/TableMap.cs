using WebFeatures.Common;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Common
{
	internal interface ITableMap
	{
		string Name { get; }
		string Schema { get; }
	}

	internal partial class TableMap : ITableMap
	{
		public TableMap(string name)
		{
			Guard.ThrowIfNullOrEmpty(name, nameof(name));

			Name = name;
		}

		public string Name { get; }
		public string Schema { get; private set; } = "public";
	}
}

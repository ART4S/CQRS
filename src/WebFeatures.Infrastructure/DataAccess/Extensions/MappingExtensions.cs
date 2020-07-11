using System;
using System.Linq.Expressions;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;

namespace WebFeatures.Infrastructure.DataAccess.Extensions
{
	internal static class MappingExtensions
	{
		public static string NameWithSchema(this ITableMap table)
		{
			return $"{table.Schema}.{table.Name}";
		}

		public static string Column<TEntity>(
			this IEntityMap<TEntity> entity,
			Expression<Func<TEntity, object>> propertyCall)
			where TEntity : class
		{
			return entity.GetProperty(propertyCall).ColumnName;
		}
	}
}

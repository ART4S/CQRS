using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Helpers
{
	internal static class SqlType<T> where T : class
	{
		private static readonly HashSet<Type> ValidSqlTypes = new HashSet<Type>
		{
			typeof(object),
			typeof(string),
			typeof(byte[]),
			typeof(long),
			typeof(int),
			typeof(short),
			typeof(bool),
			typeof(decimal),
			typeof(double),
			typeof(float),
			typeof(DateTime),
			typeof(DateTimeOffset),
			typeof(TimeSpan),
			typeof(Guid)
		};

		static SqlType()
		{
			Properties = typeof(T).GetProperties()
			   .Where(x => IsValidSqlType(x.PropertyType))
			   .ToList();
		}

		public static ICollection<PropertyInfo> Properties { get; }

		private static bool IsValidSqlType(Type type)
		{
			return
				ValidSqlTypes.Contains(type)
			 || type.IsGenericType
			 && type.GetGenericTypeDefinition() == typeof(Nullable<>)
			 && ValidSqlTypes.Contains(type.GetGenericArguments()[0])
			 || type.IsEnum;
		}
	}
}

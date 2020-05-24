using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Utils
{
    internal static class SqlType<T> where T : class
    {
        public static ICollection<PropertyInfo> Properties { get; }

        static SqlType()
        {
            Properties = typeof(T).GetProperties()
                .Where(x => IsValidSqlType(x.PropertyType))
                .ToList();
        }

        private static bool IsValidSqlType(Type type)
        {
            return
                ValidSqlTypes.Contains(type) ||
                (type.IsGenericType &&
                 type.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                 ValidSqlTypes.Contains(type.GetGenericArguments()[0])) ||
                type.IsEnum;
        }

        private static HashSet<Type> ValidSqlTypes = new HashSet<Type>()
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
            typeof(Guid),
        };
    }
}

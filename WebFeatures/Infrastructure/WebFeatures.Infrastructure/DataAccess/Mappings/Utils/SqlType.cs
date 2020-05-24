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
            return ValidSqlTypes.Contains(type) || type.IsEnum;
        }

        private static HashSet<Type> ValidSqlTypes = new HashSet<Type>()
        {
            typeof(object),

            typeof(string),

            typeof(byte[]),

            typeof(long),
            typeof(long?),

            typeof(int),
            typeof(int?),

            typeof(short),
            typeof(short?),

            typeof(bool),
            typeof(bool?),

            typeof(decimal),
            typeof(decimal?),

            typeof(double),
            typeof(double?),

            typeof(float),
            typeof(float?),

            typeof(DateTime),
            typeof(DateTime?),

            typeof(DateTimeOffset),
            typeof(DateTimeOffset?),

            typeof(TimeSpan),
            typeof(TimeSpan?),

            typeof(Guid),
            typeof(Guid?)
        };
    }
}

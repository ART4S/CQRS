using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace WebFeatures.Infrastructure.Tests.Helpers
{
    internal static class SqlBuilder
    {
        private const string Schema = "public";

        public static string DropDatabase(string databaseName)
        {
            return $"DROP DATABASE IF EXISTS {databaseName};";
        }

        public static string CreateDatabase(string databaseName)
        {
            return $"CREATE DATABASE {databaseName};";
        }

        public static string CloseExistingConnections(string databaseName)
        {
            return
                "SELECT pg_terminate_backend(pid)\n" +
                "FROM pg_stat_activity\n" +
                $"WHERE datname = '{databaseName}' AND pid <> pg_backend_pid();\n";
        }

        public static string CreateSchema()
        {
            return File.ReadAllText("Scripts/Schema.sql");
        }

        public static string Insert<TEntity>() where TEntity : class
        {
            return
                $"INSERT INTO {Schema}.{GetConventionalTableName<TEntity>()}\n" +
                $"({BuildInsertFields<TEntity>()})\n" +
                $"VALUES ({BuildInsertParams<TEntity>()})";
        }

        private static string GetConventionalTableName<TEntity>() where TEntity : class
        {
            string entityName = typeof(TEntity).Name;

            if (entityName.EndsWith("y"))
            {
                return entityName.Remove(entityName.Length - 1) + "ies";
            }

            if (entityName.EndsWith("s"))
            {
                return entityName + "es";
            }

            return entityName + "s";
        }

        private static string BuildInsertFields<TEntity>() where TEntity : class
        {
            return string.Join(", ", SqlType<TEntity>.Properties.Select(x => x.Name));
        }

        private static string BuildInsertParams<TEntity>() where TEntity : class
        {
            return string.Join(", ", SqlType<TEntity>.Properties.Select(x => "@" + x.Name));
        }

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
}
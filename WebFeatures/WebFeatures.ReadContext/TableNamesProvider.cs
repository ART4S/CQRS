using System;
using System.Collections.Generic;
using WebFeatures.Domian.Entities;

namespace WebFeatures.ReadContext
{
    internal static class TableNamesProvider
    {
        private static Dictionary<Type, string> TableNames = new Dictionary<Type, string>()
        {
            { typeof(Product), "Products" }
        };

        public static string GetTableNameForType(Type type)
        {
            if (TableNames.TryGetValue(type, out string name))
                return name;

            throw new ArgumentException($"Table name for type {type} doesn't exists");
        }
    }
}

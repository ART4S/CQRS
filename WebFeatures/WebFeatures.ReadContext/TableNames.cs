using System;
using System.Collections.Generic;
using WebFeatures.Domian.Entities;

namespace WebFeatures.ReadContext
{
    internal static class TableNames
    {
        private static Dictionary<Type, string> Names = new Dictionary<Type, string>()
        {
            { typeof(Product), "Products" }
        };

        public static string Get(Type type)
        {
            if (Names.TryGetValue(type, out string name))
                return name;

            throw new ArgumentException($"Table name for type {type} doesn't exists");
        }
    }
}

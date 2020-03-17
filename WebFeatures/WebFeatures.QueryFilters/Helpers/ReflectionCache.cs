using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace WebFeatures.QueryFilters.Helpers
{
    internal static class ReflectionCache
    {
        private static readonly ConcurrentDictionary<Type, PropertyInfo[]> Properties = new ConcurrentDictionary<Type, PropertyInfo[]>();
        private static readonly ConcurrentDictionary<string, MethodInfo> Methods = new ConcurrentDictionary<string, MethodInfo>();

        public static MethodInfo Lambda => Methods.GetOrAdd("Lambda",
            n => typeof(Expression).GetMethods().First(x => x.Name == n));

        public static MethodInfo OrderBy => Methods.GetOrAdd("OrderBy",
            n => typeof(Queryable).GetMethods().First(x => x.Name == n && x.GetParameters().Length == 2));

        public static MethodInfo OrderByDescending => Methods.GetOrAdd("OrderByDescending",
            n => typeof(Queryable).GetMethods().First(x => x.Name == n && x.GetParameters().Length == 2));

        public static MethodInfo ThenBy => Methods.GetOrAdd("ThenBy",
            n => typeof(Queryable).GetMethods().First(x => x.Name == n && x.GetParameters().Length == 2));

        public static MethodInfo ThenByDescending => Methods.GetOrAdd("ThenByDescending",
            n => typeof(Queryable).GetMethods().First(x => x.Name == n && x.GetParameters().Length == 2));

        public static MethodInfo Skip => Methods.GetOrAdd("Skip",
            n => typeof(Queryable).GetMethods().First(x => x.Name == n && x.GetParameters().Length == 2));

        public static MethodInfo Take => Methods.GetOrAdd("Take",
            n => typeof(Queryable).GetMethods().First(x => x.Name == n && x.GetParameters().Length == 2));

        public static MethodInfo Select => Methods.GetOrAdd("Select",
            n => typeof(Queryable).GetMethods().First(x => x.Name == n && x.GetParameters().Length == 2));

        public static MethodInfo Where => Methods.GetOrAdd("Where",
            n => typeof(Queryable).GetMethods().First(x => x.Name == n && x.GetParameters().Length == 2));

        public static MethodInfo ToUpper => Methods.GetOrAdd("ToUpper",
            n => typeof(string).GetMethods().First(x => x.Name == n && x.GetParameters().Length == 0));

        public static MethodInfo ToLower => Methods.GetOrAdd("ToLower",
            n => typeof(string).GetMethods().First(x => x.Name == n && x.GetParameters().Length == 0));

        public static MethodInfo StartsWith => Methods.GetOrAdd("StartsWith",
            n => typeof(string).GetMethods().First(x => x.Name == n && x.GetParameters()[0].ParameterType == typeof(string)));

        public static MethodInfo EndsWith => Methods.GetOrAdd("EndsWith",
            n => typeof(string).GetMethods().First(x => x.Name == n && x.GetParameters()[0].ParameterType == typeof(string)));

        public static PropertyInfo[] GetCashedProperties(this Type sourceType)
            => Properties.GetOrAdd(sourceType, t => t.GetProperties());
    }
}

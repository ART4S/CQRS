using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace WebFeatures.QueryFilters.Infrastructure
{
    internal static class DynamicTypeBuilder
    {
        private static readonly ConcurrentDictionary<string, Type> DynamicTypes = new ConcurrentDictionary<string, Type>();

        private static readonly ModuleBuilder ModelBuilder = AssemblyBuilder
            .DefineDynamicAssembly(new AssemblyName { Name = "DynamicTypeAssembly" }, AssemblyBuilderAccess.Run)
            .DefineDynamicModule("DynamicTypeModule");

        public static Type CreateDynamicType(IDictionary<string, Type> fields)
        {
            if (fields.Count == 0)
            {
                throw new ArgumentException(nameof(fields));
            }

            return DynamicTypes.GetOrAdd(
                GetTypeName(fields),
                typeName =>
                {
                    var typeBuilder = ModelBuilder.DefineType(
                        typeName,
                        TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.Serializable);

                    foreach (var field in fields)
                    {
                        typeBuilder.DefineField(field.Key, field.Value, FieldAttributes.Public);
                    }

                    return typeBuilder.CreateType();
                });
        }

        private static string GetTypeName(IDictionary<string, Type> properties)
        {
            return $"[{string.Join(", ", properties.OrderBy(x => x.Value).ThenBy(x => x.Key).Select(x => $"{x.Key}: {x.Value}"))}]";
        }
    }
}

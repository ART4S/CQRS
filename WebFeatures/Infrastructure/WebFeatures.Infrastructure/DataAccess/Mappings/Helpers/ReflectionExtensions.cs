using System;
using System.Linq.Expressions;
using WebFeatures.Common;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Helpers
{
    internal static class ReflectionExtensions
    {
        public static bool IsSubclassOfGeneric(this Type sourceType, Type genericTypeDefinition)
        {
            if (sourceType?.BaseType == null) return false;
            if (sourceType.BaseType == typeof(object)) return false;
            if (sourceType.BaseType.IsGenericType && sourceType.BaseType.GetGenericTypeDefinition() == genericTypeDefinition) return true;

            return IsSubclassOfGeneric(sourceType.BaseType, genericTypeDefinition);
        }

        public static string GetPropetyName<T>(this Expression<Func<T, object>> propertyCall)
        {
            Guard.ThrowIfNull(propertyCall, nameof(propertyCall));

            if (!(propertyCall.Body is MemberExpression memberAccess))
                throw new InvalidOperationException("Invalid property access");

            return memberAccess.Member.Name;
        }
    }
}

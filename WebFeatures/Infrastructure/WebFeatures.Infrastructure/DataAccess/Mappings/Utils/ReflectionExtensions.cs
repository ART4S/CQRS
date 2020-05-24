using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using WebFeatures.Common;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Utils
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

        public static string GetPropertyName<T>(this Expression<Func<T, object>> propertyCall)
        {
            Guard.ThrowIfNull(propertyCall, nameof(propertyCall));

            MemberExpression memberExpr;

            switch (propertyCall.Body)
            {
                case MemberExpression m:
                    memberExpr = m;
                    break;

                case UnaryExpression unary when unary.Operand is MemberExpression m:
                    memberExpr = m;
                    break;

                default:
                    throw new InvalidOperationException("Invalid property access");
            }

            IEnumerable<string> propertyCallChain = GetPropertyNames(memberExpr);

            return string.Join("_", propertyCallChain);
        }

        private static IEnumerable<string> GetPropertyNames(MemberExpression sourceMemberExpr)
        {
            var propertyNames = new List<string>();

            Expression current = sourceMemberExpr;

            while (current is MemberExpression memberExpr)
            {
                var property = memberExpr.Member as PropertyInfo;

                if (property == null)
                {
                    throw new InvalidOperationException("Invalid property access");
                }

                propertyNames.Add(property.Name);
                current = memberExpr.Expression;
            }

            propertyNames.Reverse();

            return propertyNames;
        }

        public static Delegate CreatePropertyAccessDelegate(this Type sourceType, PropertyInfo property)
        {
            ParameterExpression param = Expression.Parameter(sourceType);
            MemberExpression prop = Expression.Property(param, property);
            LambdaExpression lambda = Expression.Lambda(prop, new[] { param });

            return lambda.Compile();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
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

            IEnumerable<string> propertyNames = GetProperties(propertyCall).Select(x => x.Name);

            return string.Join("_", propertyNames);
        }

        public static PropertyInfo GetFirstProperty<T>(this Expression<Func<T, object>> propertyCall)
        {
            return GetProperties(propertyCall).First();
        }

        private static IEnumerable<PropertyInfo> GetProperties(LambdaExpression propertyCallExpr)
        {
            Expression current;

            switch (propertyCallExpr.Body)
            {
                case MemberExpression memberExpr:
                    current = memberExpr;
                    break;

                case UnaryExpression unary when unary.Operand is MemberExpression memberExpr:
                    current = memberExpr;
                    break;

                default:
                    throw new InvalidOperationException("Invalid property access");
            }

            var properties = new List<PropertyInfo>();

            while (current is MemberExpression memberExpr)
            {
                var property = memberExpr.Member as PropertyInfo;

                if (property == null)
                {
                    throw new InvalidOperationException("Invalid property access");
                }

                properties.Add(property);

                current = memberExpr.Expression;
            }

            properties.Reverse();

            return properties;
        }

        public static Func<T, object> CreateGetter<T>(this PropertyInfo property) where T : class
        {
            ParameterExpression param = Expression.Parameter(typeof(T));
            MemberExpression prop = Expression.Property(param, property);
            UnaryExpression converted = Expression.Convert(prop, typeof(object));

            Expression<Func<T, object>> lambda = Expression.Lambda<Func<T, object>>(converted, new[] { param });

            return lambda.Compile();
        }

        public static Action<T, object> CreateSetter<T>(this PropertyInfo property)
        {
            ParameterExpression propSource = Expression.Parameter(typeof(T));
            ParameterExpression propValue = Expression.Parameter(typeof(object));
            MemberExpression prop = Expression.Property(propSource, property);
            UnaryExpression convertedPropValue = Expression.Convert(propValue, property.PropertyType);
            BinaryExpression body = Expression.Assign(prop, convertedPropValue);

            Expression<Action<T, object>> lambda = Expression.Lambda<Action<T, object>>(
                body,
                new[] { propSource, propValue });

            return lambda.Compile();
        }

        public static Action<T, object> CreateSetter<T>(this Expression<Func<T, object>> propertyCall)
        {
            MemberExpression current;

            switch (propertyCall.Body)
            {
                case MemberExpression memberExpr:
                    current = memberExpr;
                    break;

                case UnaryExpression unary when unary.Operand is MemberExpression memberExpr:
                    current = memberExpr;
                    break;

                default:
                    throw new InvalidOperationException("Invalid property access");
            }

            ParameterExpression propSource = propertyCall.Parameters.Single();
            ParameterExpression propValue = Expression.Parameter(typeof(object));

            UnaryExpression convertedPropValue = Expression.Convert(propValue, current.Type);
            BinaryExpression body = Expression.Assign(current, convertedPropValue);

            Expression<Action<T, object>> lambda = Expression.Lambda<Action<T, object>>(
                body,
                new[] { propSource, propValue });

            return lambda.Compile();
        }
    }
}

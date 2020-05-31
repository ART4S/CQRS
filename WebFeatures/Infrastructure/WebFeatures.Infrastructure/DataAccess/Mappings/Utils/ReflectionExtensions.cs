﻿using System;
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

        public static Func<T, object> CreateGetter<T>(this PropertyInfo property)
        {
            ParameterExpression source;

            UnaryExpression body = Expression.Convert(
                Expression.Property(
                    source = Expression.Parameter(typeof(T)),
                    property),
                typeof(object));

            Expression<Func<T, object>> lambda = Expression.Lambda<Func<T, object>>(
                body,
                new[] { source });

            return lambda.Compile();
        }

        public static Action<T, object> CreateSetter<T>(this PropertyInfo property)
        {
            ParameterExpression source;
            ParameterExpression propertyValue;

            BinaryExpression body = Expression.Assign(
                Expression.Property(
                    source = Expression.Parameter(typeof(T)), property),
                Expression.Convert(
                    propertyValue = Expression.Parameter(typeof(object)),
                    property.PropertyType));

            Expression<Action<T, object>> lambda = Expression.Lambda<Action<T, object>>(
                body,
                new[] { source, propertyValue });

            return lambda.Compile();
        }

        public static Action<T, object> CreateSetter<T>(this Expression<Func<T, object>> propertyCall)
        {
            MemberExpression propertyCallExpr = GetPropertyCallExpression(propertyCall);

            ParameterExpression source = propertyCall.Parameters.Single();
            ParameterExpression propertyValue;

            BinaryExpression body = Expression.Assign(
                propertyCallExpr,
                Expression.Convert(
                    propertyValue = Expression.Parameter(typeof(object)),
                    propertyCallExpr.Type));

            Expression<Action<T, object>> lambda = Expression.Lambda<Action<T, object>>(
                body,
                new[] { source, propertyValue });

            return lambda.Compile();
        }

        private static IEnumerable<PropertyInfo> GetProperties(LambdaExpression propertyCall)
        {
            Expression current = GetPropertyCallExpression(propertyCall);

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

        private static MemberExpression GetPropertyCallExpression(LambdaExpression propertyCallExpr)
        {
            switch (propertyCallExpr.Body)
            {
                case MemberExpression memberExpr:
                    return memberExpr;

                case UnaryExpression unary when unary.Operand is MemberExpression memberExpr:
                    return memberExpr;

                default:
                    throw new InvalidOperationException("Invalid property access");
            }
        }
    }
}

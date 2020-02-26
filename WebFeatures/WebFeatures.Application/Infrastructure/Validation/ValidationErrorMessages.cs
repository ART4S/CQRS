using System;
using System.ComponentModel;
using System.Reflection;

namespace WebFeatures.Application.Infrastructure.Validation
{
    public static class ValidationErrorMessages
    {
        public static string MissingInDataBase(Type entityType)
            => $"{entityType.GetCustomAttribute<DescriptionAttribute>().Description} не найден";

        public const string NotEmpty = "Значение не должно быть пустым";
    }
}

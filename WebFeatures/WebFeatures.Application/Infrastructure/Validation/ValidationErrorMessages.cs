using System;
using System.ComponentModel;
using System.Reflection;

namespace WebFeatures.Application.Infrastructure.Validation
{
    /// <summary>
    /// Сообщения об ошибках валидации
    /// </summary>
    public static class ValidationErrorMessages
    {
        public static string NotExistsInDatabase(Type entityType)
            => $"{entityType.GetCustomAttribute<DescriptionAttribute>().Description} отсутствует в базе данных";

        public const string NotEmpty = "Значение не должно быть пустым";
        public const string InvalidLoginOrPassword = "Неверный логин или пароль";
    }
}

using System.Collections.Generic;

namespace WebFeatures.Application.Constants
{
    internal static class ValidationConstants
    {
        public static class Products
        {
            public static ICollection<string> AllowedPictureFormats =
                new HashSet<string>() { ".jpg", ".jpeg" };

            public static string PictureFormatError =
                $"Файлы следующих форматов доступны для загрузки: {string.Join(", ", AllowedPictureFormats)}";
        }
    }
}

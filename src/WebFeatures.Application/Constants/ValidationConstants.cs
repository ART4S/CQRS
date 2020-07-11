using System.Collections.Generic;

namespace WebFeatures.Application.Constants
{
	internal static class ValidationConstants
	{
		public static class Products
		{
			public static readonly ICollection<string> AllowedPictureFormats =
				new HashSet<string>
				{
					".jpg",
					".jpeg"
				};

			public static readonly string PictureFormatError =
				$"Файлы следующих форматов доступны для загрузки: {string.Join(", ", AllowedPictureFormats)}";
		}
	}
}

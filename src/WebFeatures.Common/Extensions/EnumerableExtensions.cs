using System.Collections.Generic;

namespace WebFeatures.Common.Extensions
{
	public static class EnumerableExtensions
	{
		public static string JoinString<T>(this IEnumerable<T> sourceEnumerable, string separator = ", ")
		{
			return string.Join(separator, sourceEnumerable);
		}
	}
}

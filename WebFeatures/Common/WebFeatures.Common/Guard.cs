using System;

namespace WebFeatures.Common
{
    public static class Guard
    {
        public static void ThrowIfNullOrWhiteSpace(string str, string paramName = null)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentException($"{paramName} cannot be null or whitespace");
            }
        }

        public static void ThrowIfNull<T>(T element, string paramName) where T : class
        {
            if (element == null)
            {
                throw new ArgumentNullException($"{paramName} cannot be null");
            }
        }
    }
}

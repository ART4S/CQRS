using System;

namespace WebFeatures.Common
{
    public static class Guard
    {
        public static void ThrowIfNullOrWhiteSpace(string str, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentException($"{parameterName} cannot be null or whitespace");
            }
        }
    }
}

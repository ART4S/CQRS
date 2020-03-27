using System;

namespace WebFeatures.Common
{
    public static class Guard
    {
        public static void NotNullOrWhitespace(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{parameterName}: value cannot be null or whitespace");
        }
    }
}

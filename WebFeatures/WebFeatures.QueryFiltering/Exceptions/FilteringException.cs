using System;

namespace WebFeatures.QueryFiltering.Exceptions
{
    public class FilteringException : Exception
    {
        public FilteringException(string message) : base(message)
        {
        }
    }
}

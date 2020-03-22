using System;

namespace WebFeatures.Domian.Exceptions
{
    public class DomianValidationException : Exception
    {
        public DomianValidationException(string message) : base(message)
        {
        }
    }
}

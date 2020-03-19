using System;

namespace WebFeatures.Application.Exceptions
{
    public class UserInformationException : Exception
    {
        public string Information { get; }

        public UserInformationException(string information)
        {
            Information = information;
        }
    }
}

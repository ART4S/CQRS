using System;

namespace WebFeatures.Application.Exceptions
{
    public class UserInfoException : Exception
    {
        public string Info { get; }

        public UserInfoException(string info)
        {
            Info = info;
        }
    }
}

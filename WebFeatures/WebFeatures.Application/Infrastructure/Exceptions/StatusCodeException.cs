﻿using System;

namespace WebFeatures.Application.Infrastructure.Exceptions
{
    public class StatusCodeException : Exception
    {
        public int StatusCode { get; }

        public StatusCodeException(string message, int statusCode = 400) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}

using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace WebFeatures.Application.Exceptions
{
    public class RequestValidationException : Exception
    {
        public Dictionary<string, List<string>> Errors { get; } = new Dictionary<string, List<string>>();

        public RequestValidationException(IEnumerable<ValidationFailure> failures)
        {
            foreach (ValidationFailure failure in failures)
            {
                if (!Errors.ContainsKey(failure.PropertyName))
                    Errors[failure.PropertyName] = new List<string>();

                Errors[failure.PropertyName].Add(failure.ErrorMessage);
            }
        }
    }
}

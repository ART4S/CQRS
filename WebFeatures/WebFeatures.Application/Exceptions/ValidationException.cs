using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace WebFeatures.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public Dictionary<string, List<string>> Errors { get; } = new Dictionary<string, List<string>>();

        public ValidationException(IEnumerable<ValidationFailure> failures)
        {
            foreach (var failure in failures)
            {
                if (!Errors.ContainsKey(failure.PropertyName))
                    Errors[failure.PropertyName] = new List<string>();

                Errors[failure.PropertyName].Add(failure.ErrorMessage);
            }
        }
    }
}

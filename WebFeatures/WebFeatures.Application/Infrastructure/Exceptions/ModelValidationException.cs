using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace WebFeatures.Application.Infrastructure.Exceptions
{
    public class ModelValidationException : Exception
    {
        public Dictionary<string, List<string>> Errors { get; } = new Dictionary<string, List<string>>();

        public ModelValidationException(IEnumerable<ValidationFailure> failures)
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

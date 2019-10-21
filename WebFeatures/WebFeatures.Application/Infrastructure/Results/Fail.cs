using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace WebFeatures.Application.Infrastructure.Results
{
    /// <summary>
    /// Ошибка запроса
    /// </summary>
    public class Fail
    {
        public string Message { get; }

        public Dictionary<string, List<string>> Errors { get; } = new Dictionary<string, List<string>>();

        public Fail(string message)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        public Fail(IEnumerable<ValidationFailure> errors)
        {
            foreach (var error in errors)
            {
                if (!Errors.ContainsKey(error.PropertyName))
                    Errors[error.PropertyName] = new List<string>();

                Errors[error.PropertyName].Add(error.ErrorMessage);
            }
        }

        public static implicit operator Fail(string str)
        {
            return new Fail(str);
        }
    }
}

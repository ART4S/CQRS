using System;
using System.Collections.Generic;
using FluentValidation.Results;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Common;

namespace WebFeatures.Application.Exceptions
{
	public class ValidationException : Exception
	{
		public ValidationException(string message)
		{
			Guard.ThrowIfNullOrEmpty(message, nameof(message));

			Error = new ValidationError(message);
		}

		public ValidationException(IEnumerable<ValidationFailure> failures)
		{
			Guard.ThrowIfNull(failures, nameof(failures));

			Error = new ValidationError(failures);
		}

		public ValidationError Error { get; }
	}
}

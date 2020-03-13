using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using WebFeatures.Application.Exceptions;
using WebFeatures.Requests;

namespace WebFeatures.Application.Middlewares
{
    /// <summary>
    /// Request data validation
    /// </summary>
    internal class ModelValidationMiddleware<TRequest, TResponse> : IRequestMiddleware<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ModelValidationMiddleware(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> HandleAsync(TRequest request, Func<TRequest, Task<TResponse>> next, CancellationToken cancellationToken)
        {
            var errors = (await Task.WhenAll(_validators.Select(x => x.ValidateAsync(request, cancellationToken))))
                .Where(x => !x.IsValid)
                .SelectMany(x => x.Errors)
                .ToList();

            if (errors.Count != 0)
            {
                throw new ModelValidationException(errors);
            }

            return await next(request);
        }
    }
}

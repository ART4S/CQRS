using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.Requests;
using ValidationException = WebFeatures.Application.Exceptions.ValidationException;

namespace WebFeatures.Application.Middlewares
{
    /// <summary>
    /// Validates request data
    /// </summary>
    internal class ValidationMiddleware<TRequest, TResponse> : IRequestMiddleware<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationMiddleware(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> HandleAsync(TRequest request, RequestDelegate<Task<TResponse>> next, CancellationToken cancellationToken)
        {
            IList<ValidationFailure> errors = await GetErrors(request);

            if (errors.Count != 0)
            {
                throw new ValidationException(errors);
            }

            return await next();
        }

        private async Task<IList<ValidationFailure>> GetErrors(TRequest request)
        {
            ValidationFailure[] errors =
                (await Task.WhenAll(_validators.Select(x => x.ValidateAsync(request))))
                .Where(x => !x.IsValid)
                .SelectMany(x => x.Errors)
                .ToArray();

            return errors;
        }
    }
}
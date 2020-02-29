using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using ValidationException = WebFeatures.Application.Exceptions.ValidationException;

namespace WebFeatures.Application.Infrastructure.Pipeline.Concerns
{
    /// <summary>
    /// Валидация входных данных запроса
    /// </summary>
    public class ValidationHandlerDecorator<TRequest, TResponse> : RequestHandlerDecoratorBase<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationHandlerDecorator(
            IRequestHandler<TRequest, TResponse> decoratee, 
            IEnumerable<IValidator<TRequest>> validators) : base(decoratee)
        {
            _validators = validators;
        }

        public override TResponse Handle(TRequest request)
        {
            var errors = _validators
                .Select(x => x.Validate(request))
                .Where(x => !x.IsValid)
                .SelectMany(x => x.Errors)
                .ToList();

            if (errors.Count != 0)
            {
                throw new ValidationException(errors);
            }

            return Decoratee.Handle(request);
        }
    }
}

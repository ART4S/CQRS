using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Infrastructure.Results;
using ValidationException = WebFeatures.Application.Infrastructure.Exceptions.ValidationException;

namespace WebFeatures.Application.Infrastructure.Pipeline.Concerns
{
    /// <summary>
    /// Валидация входных данных запроса
    /// </summary>
    public class ValidationHandlerDecorator<TIn, TOut> : HandlerDecoratorBase<TIn, TOut>
    {
        private readonly IEnumerable<IValidator<TIn>> _validators;

        public ValidationHandlerDecorator(IHandler<TIn, TOut> decoratee, IEnumerable<IValidator<TIn>> validators) : base(decoratee)
        {
            _validators = validators;
        }

        public override TOut Handle(TIn input)
        {
            var errors = _validators
                .Select(x => x.Validate(input))
                .Where(x => !x.IsValid)
                .SelectMany(x => x.Errors)
                .ToList();

            if (errors.Count != 0)
            {
                throw new ValidationException(new Fail(errors));
            }

            return Decoratee.Handle(input);
        }
    }
}

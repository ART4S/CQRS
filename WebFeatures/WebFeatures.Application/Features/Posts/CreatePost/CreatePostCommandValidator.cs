using FluentValidation;
using WebFeatures.Application.Infrastructure.Validation;
using WebFeatures.Application.Interfaces.Data;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.CreatePost
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator(IAppContext context)
        {
            RuleFor(x => x.BlogId)
                .Must(context.Exists<Blog, int>)
                .WithMessage(b => ValidationErrorMessages.NotExistsInDatabase(typeof(Blog)));
        }
    }
}

using FluentValidation;
using WebFeatures.Application.Infrastructure.Validation;
using WebFeatures.Application.Interfaces;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.CreatePost
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator(IRepository<Blog, int> blogRepo)
        {
            RuleFor(x => x.BlogId)
                .Must(blogRepo.Exists)
                .WithMessage(b => ValidationErrorMessages.MissingInDataBase(typeof(Blog)));
        }
    }
}

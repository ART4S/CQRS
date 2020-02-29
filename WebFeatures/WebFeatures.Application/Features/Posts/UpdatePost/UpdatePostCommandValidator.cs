using FluentValidation;
using WebFeatures.Application.Infrastructure.Validation;
using WebFeatures.Application.Interfaces;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.UpdatePost
{
    public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
    {
        public UpdatePostCommandValidator(IRepository<Post, int> postRepo)
        {
            RuleFor(x => x.Id)
                .Must(postRepo.Exists)
                .WithMessage(ValidationErrorMessages.MissingInDataBase(typeof(Post)));

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty);

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty);
        }
    }
}

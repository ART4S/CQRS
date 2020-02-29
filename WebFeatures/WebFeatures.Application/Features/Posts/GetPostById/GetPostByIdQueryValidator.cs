using FluentValidation;
using WebFeatures.Application.Infrastructure.Validation;
using WebFeatures.Application.Interfaces;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.GetPostById
{
    public class GetPostByIdQueryValidator : AbstractValidator<GetPostByIdQuery>
    {
        public GetPostByIdQueryValidator(IRepository<Post, int> postRepo)
        {
            RuleFor(x => x.Id)
                .Must(postRepo.Exists)
                .WithMessage(ValidationErrorMessages.MissingInDataBase(typeof(Post)));
        }
    }
}

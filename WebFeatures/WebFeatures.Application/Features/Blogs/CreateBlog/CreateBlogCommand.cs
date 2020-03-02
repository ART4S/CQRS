using AutoMapper;
using FluentValidation;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Infrastructure.Validation;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Blogs.CreateBlog
{
    public class CreateBlogCommand : ICommand<Unit>, IHasMappings
    {
        public int AuthorId { get; set; }
        public string Title { get; set; }

        public void ApplyMappings(Profile profile)
        {
            profile.CreateMap<CreateBlogCommand, Blog>(MemberList.Source);
        }

        public class Validator : AbstractValidator<CreateBlogCommand>
        {
            public Validator(IRepository<User, int> userRepo)
            {
                RuleFor(x => x.Title)
                    .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty);

                RuleFor(x => x.AuthorId)
                    .Must(userRepo.Exists)
                    .WithMessage(ValidationErrorMessages.MissingInDataBase(typeof(User)));
            }
        }
    }
}

using AutoMapper;
using FluentValidation;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Infrastructure.Validation;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.UpdatePost
{
    public class UpdatePostCommand : ICommand<Unit>, IHasMappings
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public void ApplyMappings(Profile profile)
        {
            profile.CreateMap<UpdatePostCommand, Post>(MemberList.Source);
        }

        public class Validator : AbstractValidator<UpdatePostCommand>
        {
            public Validator(IRepository<Post, int> postRepo)
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
}

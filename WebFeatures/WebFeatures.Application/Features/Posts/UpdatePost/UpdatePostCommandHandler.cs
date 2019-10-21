using AutoMapper;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.UpdatePost
{
    public class UpdatePostCommandHandler : ICommandHandler<UpdatePostCommand, Unit>
    {
        private readonly IAppContext _context;
        private readonly IMapper _mapper;

        public UpdatePostCommandHandler(IAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Unit Handle(UpdatePostCommand input)
        {
            var post = _context.GetById<Post>(input.Id);
            _mapper.Map(input, post);

            return Unit.Value;
        }
    }
}

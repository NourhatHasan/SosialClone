using AutoMapper;
using Context;
using FluentValidation;
using MediatR;
using RepositoryAplication.Tools;
using sosialClone;

namespace RepositoryAplication.Activities
{
    public class update
    {
        public class Comand : IRequest<result<Unit>>
        {
            public Entities entities { get; set; }
        };


        public class ComandValidation : AbstractValidator<Comand>
        {
            public ComandValidation()
            {
                RuleFor(x => x.entities).SetValidator(new Validation());

            }
        }

        public class handler : IRequestHandler<Comand,result<Unit>>
            {
                private readonly DataContext _dataContext;
                private readonly IMapper mapper;

              
                public handler(DataContext dataContext, IMapper mapper)
                {
                    _dataContext = dataContext;
                    this.mapper = mapper;
                }

            public async Task<result<Unit>> Handle(Comand request, CancellationToken cancellationToken)
            {
                var activty = await _dataContext.entities.FindAsync(request.entities.Id);
                if(activty == null)
                {
                    return null;
                }
                mapper.Map(request.entities, activty);
                var res = await _dataContext.SaveChangesAsync();
                if (res > 0)
                {
                    return result<Unit>.isSucses(Unit.Value);
                }

                return result<Unit>.Failiere("failed to update data");
            }
        }
    }
}


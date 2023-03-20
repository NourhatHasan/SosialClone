

using Context;
using FluentValidation;
using MediatR;
using RepositoryAplication.Tools;
using sosialClone;

namespace RepositoryAplication.Activities
{
    public class Create
    {

        public class Comand:IRequest<result<Unit>>
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

            public handler(DataContext dataContext)
            {
                _dataContext = dataContext;
            }

            public async Task<result<Unit>> Handle(Comand request, CancellationToken cancellationToken)
            {
                _dataContext.entities.Add(request.entities);
                var res = await _dataContext.SaveChangesAsync();
                if (res > 0)
                {
                    return result<Unit>.isSucses(Unit.Value);
                  // return new result<Unit> { success= true, data=Unit.Value};
                }
                return result<Unit>.Failiere("failed to add a new activity");


            }
        }
    }
}

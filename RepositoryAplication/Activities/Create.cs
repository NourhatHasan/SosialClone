

using Context;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RepositoryAplication.SecretInterfaces;
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
            private readonly userInterface _userInterface;

            public handler(DataContext dataContext, userInterface userInterface)
            {
                _dataContext = dataContext;
                _userInterface = userInterface;
            }

            public async Task<result<Unit>> Handle(Comand request, CancellationToken cancellationToken)
            {

                var Hosteduser= await _dataContext.Users.FirstOrDefaultAsync(x=>x.UserName== _userInterface.getUserName());

                var activityUser = new EntityUser
                {
                    user = Hosteduser,
                    entities = request.entities,
                    isHost = true
                };

               request.entities.users.Add(activityUser);

               
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



using Context;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RepositoryAplication.SecretInterfaces;
using sosialClone;

namespace RepositoryAplication.Activities
{
    public class UpdateUsers
    {
        public class Comand : IRequest<result<Unit>>
        {
           public Guid Id { get; set; }
        };





        public class handler : IRequestHandler<Comand, result<Unit>>
        {
            private readonly IUserAccesor userAccesor;
            private readonly DataContext dataContext;

            public handler( IUserAccesor userAccesor, DataContext dataContext)
            {
                this.userAccesor = userAccesor;
                this.dataContext = dataContext;
            }

            public async Task<result<Unit>> Handle(Comand request, CancellationToken cancellationToken)
            {
                var activity = await dataContext.entities
                    .Include(x=>x.Attendies)
                    .ThenInclude(u=>u.AppUser)
                    .FirstOrDefaultAsync(x=>x.Id==request.Id);

                var user = await dataContext.Users.FirstOrDefaultAsync(x=>x.UserName== userAccesor.getUserName());

                if(user == null || activity==null) 
                {
                    return null;
                }
                var hostUserName= activity.Attendies.FirstOrDefault(x=>x.isHost).AppUser.UserName;


                //sjekker om vi har user som atendy i activity
                var atendy = activity.Attendies.FirstOrDefault(x => x.AppUser.UserName == user.UserName);

                if(atendy!=null && hostUserName==user.UserName)
                {
                   activity.isCancled= !activity.isCancled;
                }
                if (atendy != null && hostUserName != user.UserName)
                {
                    activity.Attendies.Remove(atendy);
                }
                if (atendy == null)
                {
                    atendy = new EntityUser
                    {
                        AppUser = user,
                        Activity = activity,
                        isHost = false
                    };
                    activity.Attendies.Add(atendy) ;
                }
                var res = await dataContext.SaveChangesAsync();
                if (res > 0)
                {
                    return result<Unit>.isSucses(Unit.Value);
                }

                return result<Unit>.Failiere("failed to update user");

            }
        }
    }
}

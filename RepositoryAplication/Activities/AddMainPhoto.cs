

using Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RepositoryAplication.SecretInterfaces;


namespace RepositoryAplication.Activities
{
    public class AddMainPhoto
    {
        public class Comand : IRequest<result<Unit>>
        {
            public string Id { get; set; }

        }
        public class handler : IRequestHandler<Comand, result<Unit>>
        {
            private readonly IUserAccesor userAccesor;
            private readonly DataContext dataContext;

            public handler(IUserAccesor userAccesor,DataContext dataContext)
            {
                this.userAccesor = userAccesor;
                this.dataContext = dataContext;
            }

            public async Task<result<Unit>> Handle(Comand request, CancellationToken cancellationToken)
            {
                var user = await dataContext.Users
                    .Include(x => x.photos).FirstOrDefaultAsync
                    (u => u.UserName == userAccesor.getUserName());

                if(user== null) { return null; }

                var currentMainUser = user.photos.FirstOrDefault(x => x.IsMain);
                if(currentMainUser == null) { return null; }

                currentMainUser.IsMain = false;

                var newIsMain= user.photos.FirstOrDefault(x => x.Id==request.Id);
                newIsMain.IsMain = true;

                var changes = await dataContext.SaveChangesAsync();
                if (changes > 0)
                {
                    return result<Unit>.isSucses(Unit.Value);


                }
                return result<Unit>.Failiere("failed to update the main photo");


            }
        }
    }
}

using AutoMapper;
using Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RepositoryAplication.DTOs;
using RepositoryAplication.SecretInterfaces;


namespace RepositoryAplication.Activities
{
    public class userProfile
    {
        public class Query : IRequest<result<ProfileDTO>>
        {
            public string username { get; set; }
        };

        public class handler : IRequestHandler<Query, result<ProfileDTO>>
        {
            private readonly IUserAccesor userAccesor;
            private readonly DataContext dataContext;
            private readonly IMapper mapper;

            public handler(IUserAccesor userAccesor, DataContext dataContext, IMapper mapper)
            {
                this.userAccesor = userAccesor;
                this.dataContext = dataContext;
                this.mapper = mapper;
            }

            public async Task<result<ProfileDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await dataContext.Users.Include(x => x.photos).
                    FirstOrDefaultAsync(x => x.UserName == request.username);

                if (user == null)
                {
                    return null;
                }
                /*  var userToReturn = new ProfileDTO
                  {
                      DisplayName = user.DisplayName,
                      username = user.UserName,
                      Bio = user.Bio,
                      Image = user.photos.FirstOrDefault(x => x.IsMain).Url,
                      photos = user.photos.ToList()
                  };
                */

                var userToReturn = mapper.Map<ProfileDTO>(user);
                return result<ProfileDTO>.isSucses(userToReturn);
            }
        }
    }
}

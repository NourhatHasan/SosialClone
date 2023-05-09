

using AutoMapper;
using Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using sosialClone;

namespace RepositoryAplication.Activities
{
    public class list
    {
        public class Query:IRequest <result<List<ActivityDTO>>>{};
        public class handler : IRequestHandler<Query,result<List<ActivityDTO>>>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper mapper;

            public handler(DataContext dataContext, IMapper mapper)
            {
                _dataContext = dataContext;
                this.mapper = mapper;
            }

            public async Task<result<List<ActivityDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var res = await _dataContext.entities
                    .Include(x=>x.Attendies)
                    .ThenInclude(u=>u.AppUser)
                    .ThenInclude(l=>l.photos)
                    .ToListAsync();
                var activityToReturn =mapper.Map<List<ActivityDTO>>(res);
                
                //mapping from entities to activityDTO
                return result<List<ActivityDTO>>.isSucses(activityToReturn);
            }
        }

    }
    
}

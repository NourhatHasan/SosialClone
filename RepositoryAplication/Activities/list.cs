

using Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using sosialClone;

namespace RepositoryAplication.Activities
{
    public class list
    {
        public class Query:IRequest <result<List<Entities>>>{};
        public class handler : IRequestHandler<Query,result<List<Entities>>>
        {
            private readonly DataContext _dataContext;

            public handler(DataContext dataContext)
            {
                _dataContext = dataContext;
            }

            public async Task<result<List<Entities>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var res = await _dataContext.entities.ToListAsync();
                return result<List<Entities>>.isSucses(res);
            }
        }

    }
    
}

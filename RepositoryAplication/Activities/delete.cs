using Context;
using MediatR;
using sosialClone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryAplication.Activities
{
   public class Delete
    {
        public class Comand : IRequest<result<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Comand,result<Unit>>
        {
           
            private readonly DataContext _dataContext;

            public Handler(DataContext dataContext)
            {
                _dataContext = dataContext;
            }

            public async Task<result<Unit>> Handle(Comand request, CancellationToken cancellationToken)
            {
               var activity= await _dataContext.entities.FindAsync(request.Id);
                if(activity== null)
                {
                    return null;
                }
                 _dataContext.Remove(activity);
                var res = await _dataContext.SaveChangesAsync();
                if (res > 0)
                {
                    return result<Unit>.isSucses(Unit.Value);
                }
                return result<Unit>.Failiere("could not Delete the activity");
            }
        }
    }
}

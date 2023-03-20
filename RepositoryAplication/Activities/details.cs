using Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using sosialClone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryAplication.Activities
{
   public class details
    {

        public class Query : IRequest <result<Entities>>
        {
            public Guid Id;
        };
        public class handler : IRequestHandler<Query,result<Entities>>
        {
            private readonly DataContext _dataContext;

            public handler(DataContext dataContext)
            {
                _dataContext = dataContext;
            }

            public async Task<result<Entities>> Handle(Query request, CancellationToken cancellationToken)
            {
                var activtiy = await _dataContext.entities.FindAsync(request.Id);
                if (activtiy == null) { return null; }
                return result<Entities>.isSucses(activtiy);
            }







            /*
           var entity=await _dataContext.entities.FindAsync(request.Id);
            if (entity == null) throw new Exception("Not Found");
            return entity;
            */
        }
        
    }
}

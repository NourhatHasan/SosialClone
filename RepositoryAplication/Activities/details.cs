using AutoMapper;
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

        public class Query : IRequest <result<ActivityDTO>>
        {
            public Guid Id;
        };
        public class handler : IRequestHandler<Query,result<ActivityDTO>>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper mapper;

            public handler(DataContext dataContext, IMapper mapper)
            {
                _dataContext = dataContext;
                this.mapper = mapper;
            }

            public async Task<result<ActivityDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var activtiy = await _dataContext.entities
                    .Include(x=>x.Attendies)
                    .ThenInclude(u=>u.AppUser)
                    .FirstOrDefaultAsync(x=>x.Id==request.Id);


                var activityToReturn = mapper.Map<ActivityDTO>(activtiy);

                if (activtiy == null) { return null; }
                return result<ActivityDTO>.isSucses(activityToReturn);
            }







            /*
           var entity=await _dataContext.entities.FindAsync(request.Id);
            if (entity == null) throw new Exception("Not Found");
            return entity;
            */
        }
        
    }
}

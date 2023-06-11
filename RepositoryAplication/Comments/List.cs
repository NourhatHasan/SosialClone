
using AutoMapper;
using Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RepositoryAplication.Activities;
using RepositoryAplication.DTO;
using System.Security.Cryptography.X509Certificates;

namespace RepositoryAplication.Comments
{
    public class List
    {
        public class Query : IRequest<result<List<CommentDTO>>> 
        {
            public Guid ActivityId { get; set; }
        };
        public class handler : IRequestHandler<Query, result<List<CommentDTO>>>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper mapper;

            public handler(DataContext dataContext, IMapper mapper)
            {
                _dataContext = dataContext;
                this.mapper = mapper;
            }

            public async Task<result<List<CommentDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var activity = await _dataContext.entities.FindAsync(request.ActivityId);
                if(activity == null) { return null; }
                var comments = await _dataContext.comments
                    .Include(u => u.Auther)
                    .ThenInclude(x=>x.photos)
                    .Where(x => x.Activity.Id == request.ActivityId)
                    .OrderByDescending(l=>l.CreatedDate)
                    .ToListAsync();
                var commentDTOs = mapper.Map<List<CommentDTO>>(comments);

                return result<List<CommentDTO>>.isSucses(commentDTOs);
            }
        }

    }

}



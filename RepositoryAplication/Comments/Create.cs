

using AutoMapper;
using Context;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RepositoryAplication.Activities;
using RepositoryAplication.DTO;
using RepositoryAplication.SecretInterfaces;
using sosialClone;

namespace RepositoryAplication.Comments
{
    public class Create
    {
        public class Comand : IRequest<result<CommentDTO>>
        {
          
            //ActivityId from the activity we are in 
            public Guid ActivityId { get; set; }
            public string Body { get; set; }
        };



        public class ComandValidation : AbstractValidator<Comand>
        {

            //to not receive an empty comment
            public ComandValidation()
            {
                RuleFor(x => x.Body).NotEmpty()
                    .WithMessage("Body is required");

            }
        }

        //we want to return from this because we want to create an Id 
        // to use it directly in frontend 
        public class handler : IRequestHandler<Comand, result<CommentDTO>>
        {
            private readonly DataContext _dataContext;
            private readonly IUserAccesor _userInterface;
            private readonly IMapper _mapper;
            public handler(DataContext dataContext, IUserAccesor userInterface,IMapper mapper)
            {
                _dataContext = dataContext;
                _userInterface = userInterface;
                _mapper = mapper;
            }

            public async Task<result<CommentDTO>> Handle(Comand request, CancellationToken cancellationToken)
            {
               //the user needs to have main pictures as well
                var User = await _dataContext.Users.Include(x=>x.photos)
                    .FirstOrDefaultAsync(x=>x.UserName== _userInterface.getUserName()); 
                if (User == null) { return null; }

                var activity = await _dataContext.entities.FindAsync(request.ActivityId);
                if (activity == null) { return null; };

                var comment = new Comment
                {
                    Activity = activity,
                    Auther = User,
                    Body = request.Body,
                  //we do not need this since we do have DataTime.UtcNow() in Comment.cs
                  // CreatedDate = DateTime.UtcNow 
                };

                activity.Comments.Add(comment);
             //   _dataContext.comments.Add(comment);
                
                var commentToReturn = _mapper.Map<CommentDTO>(comment);

               var res= await _dataContext.SaveChangesAsync();


                if (res > 0)
                {
                    return result<CommentDTO>.isSucses(commentToReturn);
                  
                }
                return result<CommentDTO>.Failiere("failed to add a new comment");





            }
        }
    }
}



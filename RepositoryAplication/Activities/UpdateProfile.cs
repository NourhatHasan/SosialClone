using AutoMapper;
using Context;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RepositoryAplication.DTO;
using RepositoryAplication.SecretInterfaces;
using RepositoryAplication.Tools;
using sosialClone;

namespace RepositoryAplication.Activities
{
    public class updateProfile
    {
        public class Comand : IRequest<result<Unit>>
        {
           public UpdateProfileDTO UpdateProfileDTO { get; set; }
        };


        public class ComandValidation : AbstractValidator<UpdateProfileDTO>
        {
            public ComandValidation()
            {
              
                RuleFor(x => x.DisplayName).MinimumLength(6);
               

            }
        }

        public class handler : IRequestHandler<Comand, result<Unit>>
        {
            private readonly DataContext _dataContext;
            private readonly IUserAccesor userAccesor;

            public handler(DataContext dataContext, IUserAccesor userAccesor)
            {
                _dataContext = dataContext;
                this.userAccesor = userAccesor;
            }

            public async Task<result<Unit>> Handle(Comand request, CancellationToken cancellationToken)
            {
                var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.UserName == userAccesor.getUserName());
                
                if (user == null)
                {
                    return null;
                }


               
                user.DisplayName = request.UpdateProfileDTO.DisplayName;
                user.Bio = request.UpdateProfileDTO.Bio;

                var res = await _dataContext.SaveChangesAsync();
                if (res > 0)
                {
                    return result<Unit>.isSucses(Unit.Value);
                }

                return result<Unit>.Failiere("failed to update data");
            }
        }
    }
}


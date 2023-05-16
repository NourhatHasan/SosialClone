using AutoMapper;
using Context;
using FluentValidation;
using MediatR;
using RepositoryAplication.Tools;
using sosialClone;

namespace RepositoryAplication.Activities
{
    public class updateProfile
    {
        public class Comand : IRequest<result<Unit>>
        {
            public AppUser appUser { get; set; }
        };


        public class ComandValidation : AbstractValidator<Comand>
        {
            public ComandValidation()
            {
                RuleFor(x => x.appUser).SetValidator(new AppValidation());

            }
        }

        public class handler : IRequestHandler<Comand, result<Unit>>
        {
            private readonly DataContext _dataContext;
           
            public handler(DataContext dataContext)
            {
                _dataContext = dataContext;
             
            }

            public async Task<result<Unit>> Handle(Comand request, CancellationToken cancellationToken)
            {
                var profile = await _dataContext.Users.FindAsync(request.appUser.Id);
                if (profile == null)
                {
                    return null;
                }


               
                profile.UserName = request.appUser.UserName;

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


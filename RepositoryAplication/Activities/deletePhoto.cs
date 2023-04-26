

using Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RepositoryAplication.photo;
using RepositoryAplication.SecretInterfaces;
using sosialClone;

namespace RepositoryAplication.Activities
{
    public class deletePhoto
    {
        public class Comand : IRequest<result<Unit>>
        {
            public string publicId { get; set; }
        }
        public class handler : IRequestHandler<Comand, result<Unit>>
        {
            private readonly DataContext dataContext;
            private readonly IUserAccesor userAccesor;
            private readonly IPhotoAccoesor photoAccoesor;

            public handler(DataContext dataContext, IUserAccesor userAccesor, IPhotoAccoesor photoAccoesor)
            {
                this.dataContext = dataContext;
                this.userAccesor = userAccesor;
                this.photoAccoesor = photoAccoesor;
            }
            
            

            public async Task<result<Unit>> Handle(Comand request, CancellationToken cancellationToken)
            {
                var user= await dataContext.Users
                    .Include(x=>x.photos).FirstOrDefaultAsync
                    (a=>a.UserName==userAccesor.getUserName());
              /*  if (!user.photos.Any(x => x.Id == request.publicId))
                {
                    return null;
                }
              */
                var photo = user.photos.FirstOrDefault(x => x.Id == request.publicId);

                if(photo == null)
                {
                    return null;
                }

                if (photo.IsMain)
                {
                    return result<Unit>.Failiere(" the photo is the main photo");
                }

                var result = photoAccoesor.deletePhoto(request.publicId);
                if(result==null) 
                { 
                    return result<Unit>.Failiere(" the photo is not deleted in cloudinary");
                }
                user.photos.Remove(photo);

                var changes = await dataContext.SaveChangesAsync();
                if (changes > 0)
                {
                    return result<Unit>.isSucses(Unit.Value);


                }
                return result<Unit>.Failiere("failed to delete the photo");
            }


        }
        }


    }


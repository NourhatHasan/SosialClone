

using Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RepositoryAplication.photo;
using RepositoryAplication.SecretInterfaces;
using sosialClone;


namespace RepositoryAplication.Activities
{
    public class AddPhoto
    {
        public class Comand : IRequest<result<Photo>>
        {
            public IFormFile File { get; set; }

        }

        public class handler : IRequestHandler<Comand, result<Photo>>
        {
            private readonly IUserAccesor userAccesor;
            private readonly DataContext dataContext;
            private readonly IPhotoAccoesor photoAccoesor;

            public handler( IUserAccesor userAccesor, DataContext dataContext, IPhotoAccoesor photoAccoesor)
            {
                this.userAccesor = userAccesor;
                this.dataContext = dataContext;
                this.photoAccoesor = photoAccoesor;
            }
            

            public async Task<result<Photo>> Handle(Comand request, CancellationToken cancellationToken)
            {

                //getting the logd in user 
                var user = await dataContext.Users.Include(x => x.photos)
                    .SingleOrDefaultAsync(a => a.UserName
                    == userAccesor.getUserName());
              
               
                if (user == null) { return null; }
                var addPhoto = await photoAccoesor.uploadPhoto(request.File);


                //create a new photo to save in DB
                var returnPhoto = new Photo
                {
                    Id = addPhoto.PublicId,
                    Url = addPhoto.PublicId
                };
                if (!user.photos.Any(x => x.IsMain))
                {
                    returnPhoto.IsMain = true;
                    
                }
               
                user.photos.Add(returnPhoto);
                var changes=await dataContext.SaveChangesAsync();
                if (changes > 0)
                {
                    return result<Photo>.isSucses(returnPhoto);

                    
                }
                return result<Photo>.Failiere("failed to add the photo");
            }
        }
    }
}

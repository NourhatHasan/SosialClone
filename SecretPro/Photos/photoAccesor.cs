using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RepositoryAplication.photo;


namespace SecretPro.Photos
{
    //uploading photo and deleting them to/from cloudinary
    public class photoAccesor : IPhotoAccoesor
    {
        private readonly Cloudinary _cloudinary;
        public photoAccesor( IOptions<CloudinarySettings> config) 
        {
            var acount = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
                );
            _cloudinary = new Cloudinary( acount );
        }

        public async Task<string> deletePhoto(string PublicId)
        {
            var deleteParams = new DeletionParams(PublicId);
            var destroy= await _cloudinary.DestroyAsync(deleteParams);
            if(destroy.Error != null)
            {
                throw new Exception(destroy.Error.Message);
                //return null;
            }
            return destroy.Result;
        }

        public async Task<uploadPhoto> uploadPhoto(IFormFile file)
        {
            if (file.Length > 0)
            {
                //save the file before it disposes after becoming a memory
                await using var stream = file.OpenReadStream();

                var uploadImage = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Width(500).Height(500).Crop("fill")
                };

                var add = await _cloudinary.UploadAsync(uploadImage);

                if (add.Error != null)
                {
                    throw new Exception(add.Error.Message);
                }

                return new uploadPhoto
                {
                    PublicId = add.PublicId,
                    Url = add.SecureUrl.ToString()
                };


            }
            return null;
        }
    }
}

using Microsoft.AspNetCore.Http;


namespace RepositoryAplication.photo
{
    public interface IPhotoAccoesor
    {
        Task<uploadPhoto> uploadPhoto(IFormFile file);
        Task<string> deletePhoto(string PublicId);
    }
}

namespace Pets_Project_Backend.CloudinaryServices
{
    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(IFormFile file);
    }
}

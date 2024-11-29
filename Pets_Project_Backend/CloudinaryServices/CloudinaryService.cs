using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Pets_Project_Backend.CloudinaryServices
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IConfiguration configuration)
        {
            var cloudName = configuration["Cloudinary:CloudName"];
            var apiKey = configuration["Cloudinary:ApiKey"];
            var apiSecret = configuration["Cloudinary:ApiSecret"];

            if(string.IsNullOrEmpty(cloudName) || string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret)) 
            {
                throw new Exception("Cloudinary configuration is missing or incomplete");
            }

            var account = new Account(cloudName, apiKey, apiSecret);
            _cloudinary = new Cloudinary(account);
        }

        //method
      public async Task<string> UploadImageAsync(IFormFile file)
{
    if (file == null || file.Length == 0)
    {
        throw new ArgumentException("File is invalid or empty.");
    }

    try
    {
        using (var stream = file.OpenReadStream())
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Height(500).Width(500).Crop("fill")
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult == null || string.IsNullOrEmpty(uploadResult.SecureUrl.ToString()))
            {
                throw new Exception("Cloudinary upload failed.");
            }

            return uploadResult.SecureUrl.ToString();
        }
    }
    catch (Exception ex)
    {
        throw new Exception($"Error uploading image: {ex.Message}");
    }
}


    }
}

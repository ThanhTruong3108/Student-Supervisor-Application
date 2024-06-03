using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using StudentSupervisorService.CloudinaryConfig;

namespace StudentSupervisorService.Service.Implement
{
    public class ImageUrlImplement : ImageUrlService
    {
        private readonly Cloudinary cloudinary;
        public ImageUrlImplement(IOptions<CloudinarySetting> setting)
        {
            var account = new Account(setting.Value.CloudName, setting.Value.ApiKey, setting.Value.ApiSecret);
            cloudinary = new Cloudinary(account);
        }

        public async Task<List<ImageUploadResult>> UploadImage(List<IFormFile> images)
        {
            try
            {
                var uploadResults = new List<ImageUploadResult>();
                var first2Images = images.Take(2).ToList(); // just take first 2 images to upload
                
                foreach (var image in first2Images)
                {
                    if (image.Length > 0)
                    {
                        using var stream = image.OpenReadStream();
                        var uploadParams = new ImageUploadParams
                        {
                            File = new FileDescription(image.FileName, stream),
                        };
                        var uploadResult = await cloudinary.UploadAsync(uploadParams);
                        uploadResults.Add(uploadResult);
                    }
                    
                }
                return uploadResults;
            } catch (Exception ex)
            {
                throw new Exception("Upload images failed: " + ex.Message);
            }
        }
            
        public async Task<DeletionResult> DeleteImage(string publicId)
        {
            var deletionParams = new DeletionParams(publicId);
            var result = await cloudinary.DestroyAsync(deletionParams);
            return result;
        }
    }
}

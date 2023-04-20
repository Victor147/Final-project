using CloudinaryDotNet.Actions;

namespace final_project.Services.Cloudinary;

public interface IImageUploader
{
    Task<ImageUploadResult> UploadImageAsync(string name, IFormFile file);
}
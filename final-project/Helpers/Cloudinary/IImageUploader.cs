using CloudinaryDotNet.Actions;

namespace final_project.Helpers.Cloudinary;

public interface IImageUploader
{
    Task<ImageUploadResult> UploadImageAsync(string name, IFormFile file);
}
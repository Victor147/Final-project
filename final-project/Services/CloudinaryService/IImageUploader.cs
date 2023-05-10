using CloudinaryDotNet.Actions;

namespace final_project.Services.CloudinaryService;

public interface IImageUploader
{
    Task<ImageUploadResult> UploadImageAsync(string name, IFormFile file);
}
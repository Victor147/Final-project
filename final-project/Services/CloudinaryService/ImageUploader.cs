using CloudinaryDotNet.Actions;

namespace final_project.Services.CloudinaryService;

public class ImageUploader : IImageUploader
{
    private readonly CloudinaryDotNet.Cloudinary _cloudinary;

    public ImageUploader(CloudinaryDotNet.Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }

    public async Task<ImageUploadResult> UploadImageAsync(string name, IFormFile file)
    {
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.Name + Guid.NewGuid(), file.OpenReadStream()),
            PublicId = file.FileName.Split('.').First()
        };
        return await _cloudinary.UploadAsync(uploadParams);
    }
}
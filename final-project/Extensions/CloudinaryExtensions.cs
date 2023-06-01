using CloudinaryDotNet;

namespace final_project.Extensions;

public static class CloudinaryExtensions
{
    public static void AddCloudinary(this WebApplicationBuilder builder)
    {
        //builder.Services.AddSingleton<Cloudinary, Cloudinary>(sp => new Cloudinary(
        //    new Account(
        //        builder.Configuration["Cloudinary:CloudName"],
        //        builder.Configuration["Cloudinary:APIKey"],
        //        builder.Configuration["Cloudinary:APISecret"])));
        builder.Services.AddSingleton<Cloudinary, Cloudinary>(sp => new Cloudinary(
            new Account(
                "djrtvdowo",
                "465147964623319",
                "ZTNEFz1WntFLR_110ag7ZmzxqzU")));
    }
}
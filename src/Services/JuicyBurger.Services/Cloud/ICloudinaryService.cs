using Microsoft.AspNetCore.Http;

namespace JuicyBurger.Services.Cloud
{
    public interface ICloudinaryService
    {
        string UploadeImage(IFormFile imageFile, string fileName);
    }
}

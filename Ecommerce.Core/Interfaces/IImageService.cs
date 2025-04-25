using Microsoft.AspNetCore.Http;

namespace Ecommerce.Core.Interfaces
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile image);
        void DeleteImage(string imageName);
    }
}

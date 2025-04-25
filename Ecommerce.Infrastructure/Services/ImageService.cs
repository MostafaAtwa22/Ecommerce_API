using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace Ecommerce.Infrastructure.Repository
{
    public class ImageService : IImageService
    {
        private readonly string _imagesPath;
        private readonly string _baseUrl;

        public ImageService(IHostingEnvironment env)
        {
            _imagesPath = Path.Combine(env.WebRootPath, "images", "products");
            _baseUrl = "/images/products";  
        }

        public async Task<string> SaveImageAsync(IFormFile image)
        {
            var imageName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            var path = Path.Combine(_imagesPath, imageName);

            using var stream = File.Create(path);
            await image.CopyToAsync(stream);

            return Path.Combine(_baseUrl, imageName);  
        }

        public void DeleteImage(string imageName)
        {
            var path = Path.Combine(_imagesPath, imageName);
            if (File.Exists(path))
            {
                File.Delete(path);  
            }
        }
    }
}

using Ecommerce.API.Attributes;
using Ecommerce.Infrastructure.FileSettings;

namespace Ecommerce.API.Dtos
{
    public class CreateProductDto : ProductDto
    {
        [AllowedExtensions(FileSettings.AllowedExtensions),
            MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile PictureUrl { get; set; } = default!;
    }
}

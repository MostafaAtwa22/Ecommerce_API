using Ecommerce.Core.Models;

namespace Ecommerce.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
    }
}

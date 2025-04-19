using Ecommerce.Core.Models.OrderAggregate;

namespace Ecommerce.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : BaseEntity;

        Task<int>Complete();
    }
}

namespace Ecommerce.Core.Interfaces
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetAsync(string basketId);
        Task<CustomerBasket?> UpdateAsync(CustomerBasket basket);
        Task<bool> DeleteAsync(string basketId);
    }
}

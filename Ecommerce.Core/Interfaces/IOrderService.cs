using Ecommerce.Core.Models.OrderAggregate;

namespace Ecommerce.Core.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethod, string basketId,
            Address shippingAddress);
        Task<IReadOnlyList<Order>> GetOrderAsync(string buyerEmail);
        Task<Order?> GetOrderByIdAsync(int id, string buyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync();
    }
}

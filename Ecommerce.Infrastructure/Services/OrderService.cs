using Ecommerce.Core.Models.OrderAggregate;

namespace Ecommerce.Infrastructure.Services
{
    public class OrderService : IOrderService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;

        public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepository)
        {
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId,
            Address shippingAddress)
        {
             var basket = await _basketRepository.GetAsync(basketId);

            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }

            var deliveredMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            var subtotal = items.Sum(item => item.Price * item.Quantity);

            var order = new Order(buyerEmail, shippingAddress, deliveredMethod!, items, subtotal);

            _unitOfWork.Repository<Order>().Add(order);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
                return null!;

            await _basketRepository.DeleteAsync(basketId);

            return order;
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrderAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}

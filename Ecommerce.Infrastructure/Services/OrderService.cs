using Ecommerce.Core.Models.OrderAggregate;

namespace Ecommerce.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepo;
        private readonly IGenericRepository<DeliveryMethod> _dmRepo;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IBasketRepository _basketRepository;

        public OrderService(IGenericRepository<Order> orderRepo, IGenericRepository<DeliveryMethod> dmRepo,
            IGenericRepository<Product> productRepo, IBasketRepository basketRepository)
        {
            _orderRepo = orderRepo;
            _dmRepo = dmRepo;
            _productRepo = productRepo;
            _basketRepository = basketRepository;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId,
            Address shippingAddress)
        {
             var basket = await _basketRepository.GetAsync(basketId);

            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await _productRepo.GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }

            var deliveredMethod = await _dmRepo.GetByIdAsync(deliveryMethodId);

            var subtotal = items.Sum(item => item.Price * item.Quantity);

            var order = new Order(buyerEmail, shippingAddress, deliveredMethod!, items, subtotal);

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

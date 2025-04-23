using Ecommerce.Core.Models.OrderAggregate;

namespace Ecommerce.Core.Specifications
{
    public class OrderByPaymentIntentIdSpecification : BaseSpecification<Order>
    {
        public OrderByPaymentIntentIdSpecification(string paymentIntentId)
            :base(o => o.PaymentIntentId == paymentIntentId)
        {
        }
    }
}

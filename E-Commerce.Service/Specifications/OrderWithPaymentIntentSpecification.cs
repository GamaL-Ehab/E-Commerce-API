using E_Commerce.Domain.Entities.Orders;

namespace E_Commerce.Service.Specifications
{
    public class OrderWithPaymentIntentSpecification : BaseSpecification<Order>
    {
        public OrderWithPaymentIntentSpecification(string paymentIntentId) : base(o => o.PaymentIntentId == paymentIntentId)
        {
        }
    }
}

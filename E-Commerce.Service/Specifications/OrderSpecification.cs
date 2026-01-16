using E_Commerce.Domain.Entities.Orders;
using System.Linq.Expressions;

namespace E_Commerce.Service.Specifications
{
    public class OrderSpecification : BaseSpecification<Order>
    {
        public OrderSpecification(Guid id, string userEmail) : base(o => o.Id == id && o.UserEmail.ToLower() == userEmail.ToLower())
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);
        }
        public OrderSpecification(string userEmail) : base(o => o.UserEmail.ToLower() == userEmail.ToLower())
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);

            AddOrderByDesc(o => o.OrderDate);
        }
    }
}

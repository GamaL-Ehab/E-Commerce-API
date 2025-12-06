using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace E_Commerce.Domain.Entities.Orders
{
    public class Order : Entity<Guid>
    {
        public Order()
        {
        }

        public Order(string userEmail, OrderAddress shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal)
        {
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
        }

        public string UserEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public OrderAddress ShippingAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public int DeliveryMethodId { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public decimal SubTotal { get; set; } //Price * Quantity

        //[NotMapped]
        //public decimal Total { get; set; } //SubTotal + Delivery Method Cost

        public decimal GetTotal() => SubTotal + DeliveryMethod.Price; //Not Mapped
    }
}

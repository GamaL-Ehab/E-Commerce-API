namespace E_Commerce.Domain.Entities.Baskets
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public IEnumerable<BasketItem> Items { get; set; }
        public int? DeliveryMethodId { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public decimal? ShippingCost { get; set; }
    }
}

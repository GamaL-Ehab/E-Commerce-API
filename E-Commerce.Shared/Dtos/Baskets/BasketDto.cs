namespace E_Commerce.Shared.Dtos.Baskets
{
    public class BasketDto
    {
        public string Id { get; set; }
        public IEnumerable<BasketItemDto> Items { get; set; }
        public int? DeliveryMethodId { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public decimal? ShippingCost { get; set; }
    }
}

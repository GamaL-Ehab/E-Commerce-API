namespace E_Commerce.Shared.Dtos.Baskets
{
    public class BasketItemDto
    {
        public int Id { get; set; }
        public string productName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
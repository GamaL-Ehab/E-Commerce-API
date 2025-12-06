namespace E_Commerce.Shared.Dtos.Baskets
{
    public class BasketDto
    {
        public string Id { get; set; }
        public IEnumerable<BasketItemDto> BasketItems { get; set; }
    }
}

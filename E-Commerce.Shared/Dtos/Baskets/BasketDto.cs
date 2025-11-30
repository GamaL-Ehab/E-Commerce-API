namespace E_Commerce.Shared.Dtos.Baskets
{
    public class BasketDto
    {
        public string Id { get; set; }
        IEnumerable<BasketItemDto> BasketItems { get; set; }
    }
}

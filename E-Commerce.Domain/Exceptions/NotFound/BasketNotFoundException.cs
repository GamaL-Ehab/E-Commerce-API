namespace E_Commerce.Domain.Exceptions.NotFound
{
    public class BasketNotFoundException(string id) : NotFoundException($"Basket with id {id} was not found!")
    {
    }
}

namespace E_Commerce.Domain.Exceptions.NotFound
{
    public class OrderNotFoundException(Guid id) : NotFoundException($"Order with id {id} was not found!")
    {
    }
}

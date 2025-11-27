namespace E_Commerce.Domain.Exceptions.NotFound
{
    public class ProductNotFoundException(int id) : NotFoundException($"Product with id {id} was not found!")
    {
    }
}

namespace E_Commerce.Domain.Exceptions.NotFound
{
    public abstract class NotFoundException(string message) : Exception(message)
    {
    }
}

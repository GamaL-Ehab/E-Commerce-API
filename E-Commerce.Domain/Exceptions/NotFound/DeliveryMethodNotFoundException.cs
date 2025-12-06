namespace E_Commerce.Domain.Exceptions.NotFound
{
    public class DeliveryMethodNotFoundException(int id) : NotFoundException($"Delivery method with id {id} was not found!")
    {
    }
}

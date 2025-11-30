namespace E_Commerce.Domain.Exceptions.BadRequest
{
    public class CreateOrUpdateBasketBadRequestException() : BadRequestException("Can not update or create basket!")
    {
    }
}

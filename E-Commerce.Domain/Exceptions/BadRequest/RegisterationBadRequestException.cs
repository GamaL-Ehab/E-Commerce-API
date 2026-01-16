namespace E_Commerce.Domain.Exceptions.BadRequest
{
    public class RegisterationBadRequestException(List<string> errors) : BadRequestException(string.Join(", ", errors))
    {
    }
}

namespace E_Commerce.Domain.Exceptions
{
    public sealed class UserNotFoundException(string email) : NotFoundException($"No User With Email: {email} was Found")
    {
    }
}

namespace E_Commerce.Domain.Exceptions
{
    public sealed class BasketNotFoundException(string id) : NotFoundException($"Basket With Id: {id} Not Found.")
    {
        
    }
}

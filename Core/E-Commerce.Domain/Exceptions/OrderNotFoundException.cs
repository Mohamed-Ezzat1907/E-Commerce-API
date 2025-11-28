namespace E_Commerce.Domain.Exceptions
{
    public sealed class OrderNotFoundException : NotFoundException
    {
        public OrderNotFoundException(Guid id)
            : base($"Order with Id: {id} was not found.")
        {
        }
    }
}

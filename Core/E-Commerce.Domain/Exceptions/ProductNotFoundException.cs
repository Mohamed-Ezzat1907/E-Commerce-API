namespace E_Commerce.Domain.Exceptions
{
    public sealed class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(int id) : base($"Product With Id: {id} Not Found")
        {
        }
    }
}

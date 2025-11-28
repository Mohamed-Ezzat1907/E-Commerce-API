namespace E_Commerce.Domain.Exceptions
{
    public sealed class DeliveryMethodException : NotFoundException
    {
        public DeliveryMethodException(int deliveryMethodId)
            : base($"Delivery Method with Id: {deliveryMethodId} was not found.")
        {
        }
    }
}

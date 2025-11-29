using E_Commerce.Domain.Entities.OrderAggregate;

namespace E_Commerce.Services.Specifications
{
    internal class OrderWithPaymentIntentIdSpecifications : BaseSpecifications<Order , Guid>
    {
        public OrderWithPaymentIntentIdSpecifications(string paymentIntentId)
            : base(o => o.PaymentIntentId == paymentIntentId)
        {
        }
    }
}

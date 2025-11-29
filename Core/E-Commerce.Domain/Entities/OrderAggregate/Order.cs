global using ShippingAdress = E_Commerce.Domain.Entities.OrderAggregate.Address;
namespace E_Commerce.Domain.Entities.OrderAggregate
{
    public class Order : BaseEntity<Guid>
    {
        #region Constructors

        public Order()
        {
            
        }

        public Order(string userEmail,
            ShippingAdress shippingAdress,
            ICollection<OrderItem> orderItems,
            DeliveryMethod deliveryMethod, 
            decimal subtotal,
            string paymentIntentId)
        {
            UserEmail = userEmail;
            ShippingAdress = shippingAdress;
            OrderItems = orderItems;
            DeliveryMethod = deliveryMethod;
            Subtotal = subtotal;
            PaymentIntentId = paymentIntentId;
        }

        #endregion

        #region Properties

        public string UserEmail { get; set; } = string.Empty;
        public Address ShippingAdress { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public OrderPaymentStatus PaymentStatus { get; set; } = OrderPaymentStatus.Pending;
        public DeliveryMethod DeliveryMethod { get; set; }

        public int? DeliveryMethodID { get; set; }

        // Subtotal = Sum(OrderItem.price * OrderItem.quantity)
        public decimal Subtotal { get; set; }
        public string PaymentIntentId { get; set; } 

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;

        #endregion
    }
}

namespace E_Commerce.Domain.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity<Guid>
    {
        #region Constructors

        public OrderItem()
        {
        }

        public OrderItem(ProductInOrderItem product, int quantity, decimal price)
        {
            Product = product;
            Quantity = quantity;
            Price = price;
        }

        #endregion

        #region Properties

        public ProductInOrderItem Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        #endregion
    }
}

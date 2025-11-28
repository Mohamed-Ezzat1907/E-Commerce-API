namespace E_Commerce.Domain.Entities.OrderAggregate
{
    public class DeliveryMethod : BaseEntity<int>
    {
        #region Constructors
        public DeliveryMethod()
        {
        }

        public DeliveryMethod(string shortName, string description, string deliveryTime, decimal price)
        {
            ShortName = shortName;
            Description = description;
            DeliveryTime = deliveryTime;
            Price = price;
        }

        #endregion

        #region Properties

        public string ShortName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DeliveryTime { get; set; } = string.Empty; // Text ==> Within 3 Days
        public decimal Price { get; set; }

        #endregion

    }
}

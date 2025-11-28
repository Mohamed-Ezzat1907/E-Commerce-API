namespace E_Commerce.Domain.Entities.OrderAggregate
{
    public class ProductInOrderItem
    {
        #region Constructors

        public ProductInOrderItem()
        {
        }

        public ProductInOrderItem(int productId, string productName, string pictureUrl)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        #endregion

        #region Properties

        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string PictureUrl { get; set; } = string.Empty;

        #endregion
    }
}

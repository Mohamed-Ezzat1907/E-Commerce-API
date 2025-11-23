namespace E_Commerce.Domain.Entities.BasketModule
{
    public class CustomerBasket
    {
        public string Id { get; set; } = string.Empty;
        public IEnumerable<BasketItem> Items { get; set; } = [];
    }
}

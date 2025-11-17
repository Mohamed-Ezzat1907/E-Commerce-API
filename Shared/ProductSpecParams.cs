namespace Shared
{
    public class ProductSpecParams
    {
        public ProductSortingOptions? Sort { get; set; }
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }

        private const int MaxPageSize = 10;
        private const int DefaultPageSize = 5;
        public int PageIndex { get; set; } = 1;

        private int pageSize = DefaultPageSize;

        public int PageSize
        {
            get => pageSize;
            set => pageSize = value > MaxPageSize ? MaxPageSize : value;
        }

        public string? Search { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace E_Commerce.Shared.Dtos.Products
{
    public class ProductQueryParameters
    {
        private const int DEFAULTPAGESIZE = 5;
        private const int MAXPAGESIZE = 10;
        private int pageSize = DEFAULTPAGESIZE;

        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Search { get; set; }
        public ProductSortingOptions Sort { get; set; }

        public int PageIndex { get; set; } = 1;
        public int PageSize
        {
            get => pageSize;
            set
            {
                pageSize = value == 0 ? 5
                         : value > MAXPAGESIZE ? MAXPAGESIZE
                         : value < DEFAULTPAGESIZE ? DEFAULTPAGESIZE : value;
            }
        }

    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ProductSortingOptions
    {
        NameAsc = 1,
        NameDesc = 2,
        PriceAsc = 3,
        PriceDesc = 4
    }
}

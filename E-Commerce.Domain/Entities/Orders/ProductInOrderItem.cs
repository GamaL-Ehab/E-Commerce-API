namespace E_Commerce.Domain.Entities.Orders
{
    //Part of orderItem table
    public class ProductInOrderItem
    {
        public ProductInOrderItem()
        {
        }

        public ProductInOrderItem(int productId, string productName, string pictureUrl)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}
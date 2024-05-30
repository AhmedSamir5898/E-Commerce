namespace Talabat.Core.Entities.Orders
{
    public class productItemOrder
    {
       

        public int productId { get; set; }
        public string PictureUrl { get; set; }
        public string ProductName { get; set; }
        public productItemOrder(int productId, string pictureUrl, string productName)
        {
            this.productId = productId;
            PictureUrl = pictureUrl;
            ProductName = productName;
        }
        public productItemOrder()
        {
            
        }
    }
}
namespace APINttShop.Models.Request
{
    public class UpdateOrderStatusRequest
    {
        public int idOrder {  get; set; }
        public int orderStatus { get; set; }
    }
}

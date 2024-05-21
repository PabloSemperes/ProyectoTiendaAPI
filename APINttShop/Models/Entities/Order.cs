namespace APINttShop.Models.Entities
{
    public class Order
    {
        public int idOrder {  get; set; }
        public DateTime dateTime { get; set; }
        public int orderStatus { get; set; }
        public decimal totalPrice { get; set; }
        public int idUser { get; set; } 
        public List<OrderDetail> orderDetails { get; set; }
    }
}

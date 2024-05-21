namespace APINttShop.Models.Entities
{
    public class OrderDetail
    {
        public int idOrder { get; set; }
        public int idProduct { get; set; }
        public decimal price { get; set; }
        public int units { get; set; }
        public Product? product { get; set; }
    }
}

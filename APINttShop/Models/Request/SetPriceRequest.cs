namespace APINttShop.Models.Request
{
    public class SetPriceRequest
    {
        public int idProduct {  get; set; }
        public int idRate { get; set; }
        public decimal price { get; set; }
    }
}

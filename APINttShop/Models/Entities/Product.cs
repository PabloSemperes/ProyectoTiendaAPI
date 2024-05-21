namespace APINttShop.Models.Entities
{
    public class Product
    {
        public int idProduct {  get; set; }
        public int stock { get; set; }
        public bool enabled { get; set; }
        public List<ProductDescription>? descriptions { get; set; }
        public List<ProductRate>? rates { get; set; }
    }
}

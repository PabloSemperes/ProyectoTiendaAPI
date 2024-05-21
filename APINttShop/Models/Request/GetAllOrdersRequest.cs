namespace APINttShop.Models.Request
{
    public class GetAllOrdersRequest
    {
        public DateTime? fromDate {  get; set; }
        public DateTime? toDate { get; set; }
        public int? orderStatus { get; set; }
    }
}

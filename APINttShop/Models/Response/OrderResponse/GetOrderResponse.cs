using API_nttshop.Models;
using APINttShop.Models.Entities;

namespace APINttShop.Models.Response.OrderResponse
{
    public class GetOrderResponse : BaseReponseModel
    {
        public Order order { get; set; }
    }
}

using API_nttshop.Models;
using APINttShop.Models.Entities;

namespace APINttShop.Models.Response.OrderResponse
{
    public class GetAllOrderStatusResponse : BaseReponseModel
    {
        public List<OrderStatus> orderStatus { get; set; }
    }
}

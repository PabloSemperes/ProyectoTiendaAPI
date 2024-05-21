using API_nttshop.Models;
using APINttShop.Models.Entities;

namespace APINttShop.Models.Response.OrderResponse
{
    public class GetAllOrdersResponse : BaseReponseModel
    {
        public List<Order> orders { get; set; }
    }
}

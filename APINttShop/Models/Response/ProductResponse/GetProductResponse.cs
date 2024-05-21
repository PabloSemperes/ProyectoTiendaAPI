using API_nttshop.Models;
using APINttShop.Models.Entities;

namespace APINttShop.Models.Response.ProductResponse
{
    public class GetProductResponse : BaseReponseModel
    {
        public Product product { get; set; }
    }
}

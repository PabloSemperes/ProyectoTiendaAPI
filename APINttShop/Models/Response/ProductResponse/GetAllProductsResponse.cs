using API_nttshop.Models;
using APINttShop.Models.Entities;

namespace APINttShop.Models.Response.ProductResponse
{
    public class GetAllProductsResponse : BaseReponseModel
    {
        public List<Product> products {  get; set; }
    }
}

using API_nttshop.Models;
using APINttShop.Models.Entities;

namespace APINttShop.Models.Response.RateResponse
{
    public class GetRateResponse : BaseReponseModel
    {
        public Rate rate { get; set; }
    }
}

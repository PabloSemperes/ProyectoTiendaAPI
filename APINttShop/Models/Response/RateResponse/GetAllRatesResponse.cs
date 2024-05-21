using API_nttshop.Models;
using APINttShop.Models.Entities;

namespace APINttShop.Models.Response.RateResponse
{
    public class GetAllRatesResponse : BaseReponseModel
    {
        public List<Rate> ratesList {  get; set; }
    }
}

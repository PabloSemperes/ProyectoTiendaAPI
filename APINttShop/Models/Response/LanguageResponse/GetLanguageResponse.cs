using API_nttshop.Models;
using API_nttshop.Models.Entities;

namespace APINttShop.Models.Response.LanguageResponse
{
    public class GetLanguageResponse : BaseReponseModel
    {
        public Language language { get; set; }
    }
}

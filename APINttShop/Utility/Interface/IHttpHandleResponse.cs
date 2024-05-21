using API_nttshop.Models;
using Microsoft.AspNetCore.Mvc;

namespace APINttShop.Utility.Interface
{
    public interface IHttpHandleResponse
    {
        public ActionResult HandleResponse(BaseReponseModel response);
    }
}

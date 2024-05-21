using API_nttshop.BC;
using API_nttshop.Models;
using API_nttshop.Models.Reponse.LanguageResponse;
using APINttShop.BC;
using APINttShop.Models.Request;
using APINttShop.Models.Response.RateResponse;
using APINttShop.Utility.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APINttShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController(IHttpHandleResponse httpHandleResponse) : Controller
    {
        private readonly RateBC rateBC = new RateBC();
        private readonly IHttpHandleResponse _httpHandleResponse = httpHandleResponse;

        [HttpGet]
        [Route("getRate")]
        public ActionResult<GetRateResponse> GetRate(IdRequest request)
        {
            GetRateResponse result = rateBC.getRate(request);

            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpGet]
        [Route("getAllRates")]
        public ActionResult<GetAllRatesResponse> GetAllRates()
        {
            GetAllRatesResponse result = rateBC.getAllRates();

            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpPost]
        [Route("insertRate")]
        public ActionResult<BaseReponseModel> InsertRate(RateRequest request)
        {
            BaseReponseModel result = rateBC.insertRate(request);

            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpPut]
        [Route("updateRate")]
        public ActionResult<BaseReponseModel> UpdateRate(RateRequest request)
        {
            BaseReponseModel result = rateBC.UpdateRate(request);

            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpDelete]
        [Route("deleteRate")]
        public ActionResult<BaseReponseModel> DeleteRate(IdRequest request)
        {
            BaseReponseModel result = rateBC.DeleteRate(request);

            return _httpHandleResponse.HandleResponse(result);
        }
    }
}

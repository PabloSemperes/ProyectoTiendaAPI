using API_nttshop.BC;
using API_nttshop.DAC;
using API_nttshop.Models;
using API_nttshop.Models.Entities;
using API_nttshop.Models.Reponse.LanguageResponse;
using APINttShop.Models.Request;
using APINttShop.Models.Response.LanguageResponse;
using APINttShop.Utility.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API_nttshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController(IHttpHandleResponse httpHandleResponse) : Controller
    {
        private readonly LanguageBC languageBC = new LanguageBC();
        private readonly IHttpHandleResponse _httpHandleResponse = httpHandleResponse;

        [HttpGet]
        [Route("getAllLanguages")]
        public ActionResult<GetAllLanguagesResponse> GetAllLenguages()
        {
            GetAllLanguagesResponse result = languageBC.getAllLanguages();

            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpGet]
        [Route("getLanguage")]
        public ActionResult<GetLanguageResponse> GetLenguage(IdRequest request)
        {
            GetLanguageResponse result = languageBC.getLanguage(request);

            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpPost]
        [Route("insertLanguage")]
        public ActionResult<BaseReponseModel> InsertLanguage(LanguageRequest request)
        {
            BaseReponseModel result = languageBC.InsertLanguage(request);

            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpPut]
        [Route("updateLanguage")]
        public ActionResult<BaseReponseModel> UpdateLanguage(LanguageRequest request)
        {
            BaseReponseModel result = languageBC.UpdateLanguage(request);

            return _httpHandleResponse.HandleResponse(result);
        }

        [HttpDelete]
        [Route("deleteLanguage")]
        public ActionResult<BaseReponseModel> DeleteLanguage(IdRequest request)
        {
            BaseReponseModel result = languageBC.DeleteLanguage(request);

            return _httpHandleResponse.HandleResponse(result);
        }
    }
}

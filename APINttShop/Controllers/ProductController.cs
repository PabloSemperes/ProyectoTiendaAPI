using API_nttshop.BC;
using API_nttshop.Models;
using API_nttshop.Models.Reponse.LanguageResponse;
using APINttShop.BC;
using APINttShop.Models.Request;
using APINttShop.Models.Response.ProductResponse;
using APINttShop.Utility;
using APINttShop.Utility.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APINttShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IHttpHandleResponse httpHandleResponse) : Controller
    {
        private readonly ProductBC productBC = new ProductBC();

        [HttpGet]
        [Route("getProduct/{id}/{lang?}")]
        public ActionResult<GetProductResponse> GetProduct(int id, string lang=null)
        {
            GetProductResponse result = productBC.getProduct(id, lang);

            return httpHandleResponse.HandleResponse(result);
        }
        [HttpGet]
        [Route("getAllProducts/{lang?}")]
        public ActionResult<GetAllProductsResponse> GetAllProducts(string lang=null)
        {
            GetAllProductsResponse result = productBC.getAllProducts(lang);

            return httpHandleResponse.HandleResponse(result);
        }
        [HttpPost]
        [Route("insertProduct")]
        public ActionResult<BaseReponseModel> InsertProduct(ProductRequest request)
        {
            BaseReponseModel result = productBC.InsertProduct(request);

            return httpHandleResponse.HandleResponse(result);
        }
        [HttpPut]
        [Route("updateProduct")]
        public ActionResult<BaseReponseModel> UpdateProduct(ProductRequest request)
        {
            BaseReponseModel result = productBC.UpdateProduct(request);

            return httpHandleResponse.HandleResponse(result);
        }
        [HttpDelete]
        [Route("deleteProduct")]
        public ActionResult<BaseReponseModel> DeleteProduct(IdRequest idRequest)
        {
            BaseReponseModel result = productBC.DeleteProduct(idRequest);

            return httpHandleResponse.HandleResponse(result);
        }
        [HttpPut]
        [Route("setPrice")]
        public ActionResult<BaseReponseModel> SetPrice(SetPriceRequest request)
        {
            BaseReponseModel result = productBC.SetPrice(request);

            return httpHandleResponse.HandleResponse(result);
        }
    }
}

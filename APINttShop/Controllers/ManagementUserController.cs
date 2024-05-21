using API_nttshop.Models;
using APINttShop.BC;
using APINttShop.Models.Request;
using APINttShop.Models.Response.ManagementUserResponse;
using APINttShop.Models.Response.UserResponse;
using APINttShop.Utility.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APINttShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementUserController(IHttpHandleResponse httpHandleResponse) : Controller
    {
        private readonly ManagementUserBC managementUserBC = new ManagementUserBC();
        private readonly IHttpHandleResponse _httpHandleResponse = httpHandleResponse;

        [HttpGet]
        [Route("getManagementUser")]
        public ActionResult<GetManagementUserResponse> GetManagementUser(IdRequest request)
        {
            GetManagementUserResponse result = managementUserBC.getManagementUser(request);

            return _httpHandleResponse.HandleResponse(result);
        }
        [HttpGet]
        [Route("getAllManagementUsers")]
        public ActionResult<GetAllManagementUsersResponse> GetAllManagementUsers()
        {
            GetAllManagementUsersResponse result = managementUserBC.getAllManagementUsers();

            return _httpHandleResponse.HandleResponse(result);
        }
        [HttpPost]
        [Route("insertManagementUser")]
        public ActionResult<BaseReponseModel> InsertManagementUser(ManagementUserRequest request)
        {
            BaseReponseModel result = managementUserBC.insertManagementUser(request);

            return _httpHandleResponse.HandleResponse(result);
        }
        [HttpPut]
        [Route("updateManagementUser")]
        public ActionResult<BaseReponseModel> UpdateManagementUser(ManagementUserRequest request)
        {
            BaseReponseModel result = managementUserBC.UpdateManagementUser(request);

            return _httpHandleResponse.HandleResponse(result);
        }
        [HttpDelete]
        [Route("deleteManagementUser")]
        public ActionResult<BaseReponseModel> DeleteManagementUser(IdRequest request)
        {
            BaseReponseModel result = managementUserBC.DeleteManagementUser(request);

            return _httpHandleResponse.HandleResponse(result);
        }
        [HttpPut]
        [Route("updateManagementUserPassword")]
        public ActionResult<BaseReponseModel> UpdateManagementUserPassword(ManagementUserRequest request)
        {
            BaseReponseModel result = managementUserBC.UpdateManagementUserPassword(request);

            return _httpHandleResponse.HandleResponse(result);
        }
        [HttpPost]
        [Route("managementUserLogin")]
        public ActionResult<BaseReponseModel> ManagementUserLogin(ManagementUserRequest request)
        {
            BaseReponseModel result = managementUserBC.ManagementUserLogin(request);

            return _httpHandleResponse.HandleResponse(result);
        }
    }
}

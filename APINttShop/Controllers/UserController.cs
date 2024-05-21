using API_nttshop.Models;
using APINttShop.BC;
using APINttShop.Models.Request;
using APINttShop.Models.Response.RateResponse;
using APINttShop.Models.Response.UserResponse;
using APINttShop.Utility.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APINttShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IHttpHandleResponse httpHandleResponse) : Controller
    {
        private readonly UserBC userBC = new UserBC();
        private readonly IHttpHandleResponse _httpHandleResponse = httpHandleResponse;

        [HttpGet]
        [Route("getUser/{id}")]
        public ActionResult<GetUserResponse> GetUser(int id)
        {
            GetUserResponse result = userBC.getUser(id);

            return _httpHandleResponse.HandleResponse(result);
        }
        [HttpGet]
        [Route("getAllUsers")]
        public ActionResult<GetAllUsersResponse> GetAllUsers()
        {
            GetAllUsersResponse result = userBC.getAllUsers();

            return _httpHandleResponse.HandleResponse(result);
        }
        [HttpPost]
        [Route("insertUser")]
        public ActionResult<BaseReponseModel> InsertUser(UserRequest request)
        {
            BaseReponseModel result = userBC.insertUser(request);

            return _httpHandleResponse.HandleResponse(result);
        }
        [HttpPut]
        [Route("updateUser")]
        public ActionResult<BaseReponseModel> UpdateUser(UserRequest request)
        {
            BaseReponseModel result = userBC.UpdateUser(request);

            return _httpHandleResponse.HandleResponse(result);
        }
        [HttpDelete]
        [Route("deleteUser")]
        public ActionResult<BaseReponseModel> DeleteUser(IdRequest request)
        {
            BaseReponseModel result = userBC.DeleteUser(request);

            return _httpHandleResponse.HandleResponse(result);
        }
        [HttpPut]
        [Route("updateUserPassword")]
        public ActionResult<BaseReponseModel> UpdateUserPassword(UserRequest request)
        {
            BaseReponseModel result = userBC.UpdateUserPassword(request);

            return _httpHandleResponse.HandleResponse(result);
        }
        [HttpPost]
        [Route("userLogin")]
        public ActionResult<BaseReponseModel> UserLogin(UserRequest request)
        {
            BaseReponseModel result = userBC.UserLogin(request);

            return _httpHandleResponse.HandleResponse(result);
        }
    }
}

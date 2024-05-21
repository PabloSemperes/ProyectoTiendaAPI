using API_nttshop.Models;

namespace APINttShop.Models.Response.UserResponse
{
    public class GetAllUsersResponse : BaseReponseModel
    {
        public List<User> usersList {  get; set; }
    }
}

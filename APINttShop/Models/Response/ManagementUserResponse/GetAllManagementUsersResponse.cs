using API_nttshop.Models;

namespace APINttShop.Models.Response.ManagementUserResponse
{
    public class GetAllManagementUsersResponse : BaseReponseModel
    {
        public List<ManagementUser> managementUsersList { get; set; }
    }
}

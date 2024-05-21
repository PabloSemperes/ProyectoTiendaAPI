using API_nttshop.Models;
using APINttShop.DAC;
using APINttShop.Models.Request;
using APINttShop.Models.Response.ManagementUserResponse;
using APINttShop.Models.Response.UserResponse;

namespace APINttShop.BC
{
    public class ManagementUserBC
    {
        private readonly ManagementUserDAC managementUserDAC = new ManagementUserDAC();
        public GetManagementUserResponse getManagementUser(IdRequest request)
        {
            GetManagementUserResponse result = new GetManagementUserResponse();

            if (GetManagementUserValidation(request))
            {
                result.managementUser = managementUserDAC.GetManagementUser(request.id);

                if (result.managementUser != null)
                {
                    result.httpStatus = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    result.httpStatus = System.Net.HttpStatusCode.NoContent;
                    result.message = "No content";
                }
            }

            return result;
        }
        private bool GetManagementUserValidation(IdRequest request)
        {
            if (request != null
                && request.id > 0)
            {
                return true;
            }
            else return false;
        }
        public GetAllManagementUsersResponse getAllManagementUsers()
        {
            GetAllManagementUsersResponse result = new GetAllManagementUsersResponse();

            result.managementUsersList = managementUserDAC.GetAllManagementUsers();

            if (result.managementUsersList.Count() > 0)
            {
                result.httpStatus = System.Net.HttpStatusCode.OK;
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.NoContent;
                result.message = "No content";
            }

            return result;
        }
        public BaseReponseModel insertManagementUser(ManagementUserRequest request)
        {
            BaseReponseModel result = new BaseReponseModel();

            if (InsertManagementUserValidation(request))
            {
                bool correctOperation = managementUserDAC.InsertManagementUser(request.managementUser);

                if (correctOperation)
                {
                    result.httpStatus = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    result.httpStatus = System.Net.HttpStatusCode.Conflict;
                    result.message = "Insert a password with at least one uppercase, lowercase and a digit. Must be at least 10 characters";
                }
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
            }

            return result;
        }

        private bool InsertManagementUserValidation(ManagementUserRequest request)
        {
            if (request != null
                && request.managementUser != null
                && !string.IsNullOrWhiteSpace(request.managementUser.Login)
                && !string.IsNullOrWhiteSpace(request.managementUser.Password)
                && !string.IsNullOrWhiteSpace(request.managementUser.Name)
                && !string.IsNullOrWhiteSpace(request.managementUser.Surname1)
                && !string.IsNullOrWhiteSpace(request.managementUser.Email)
               )
            {
                return true;
            }
            else return false;
        }
        public BaseReponseModel UpdateManagementUser(ManagementUserRequest request)
        {
            BaseReponseModel result = new BaseReponseModel();

            if (UpdateManagementUserValidation(request))
            {
                bool correctOperation = managementUserDAC.UpdateManagementUser(request.managementUser);

                if (correctOperation)
                {
                    result.httpStatus = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    result.httpStatus = System.Net.HttpStatusCode.NotFound;
                }
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
            }


            return result;
        }

        private bool UpdateManagementUserValidation(ManagementUserRequest request)
        {
            if (request != null
                && request.managementUser != null
                && !string.IsNullOrWhiteSpace(request.managementUser.Login)
                && !string.IsNullOrWhiteSpace(request.managementUser.Name)
                && !string.IsNullOrWhiteSpace(request.managementUser.Surname1)
                && !string.IsNullOrWhiteSpace(request.managementUser.Email)
               )
            {
                return true;
            }
            else return false;
        }
        public BaseReponseModel DeleteManagementUser(IdRequest request)
        {
            BaseReponseModel result = new BaseReponseModel();

            if (DeleteManagementUserValidation(request))
            {
                bool correctOperation = managementUserDAC.DeleteManagementUser(request.id);

                if (correctOperation)
                {
                    result.httpStatus = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    result.httpStatus = System.Net.HttpStatusCode.NotFound;
                }
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
            }


            return result;
        }
        private bool DeleteManagementUserValidation(IdRequest request)
        {
            if (request != null
                && request.id > 0)
            {
                return true;
            }
            else return false;
        }
        public BaseReponseModel UpdateManagementUserPassword(ManagementUserRequest request)
        {
            BaseReponseModel result = new BaseReponseModel();

            if (UpdateManagementUserValidation(request))
            {
                byte correctOperation = managementUserDAC.UpdateManagementUserPassword(request.managementUser);

                switch (correctOperation)
                {
                    case 0:
                        result.httpStatus = System.Net.HttpStatusCode.NotFound;
                        break;
                    case 1:
                        result.httpStatus = System.Net.HttpStatusCode.OK;
                        break;
                    case 2:
                        result.httpStatus = System.Net.HttpStatusCode.Conflict;
                        result.message = "Insert a password with at least one uppercase, lowercase and a digit. Must be at least 10 characters";
                        break;
                    case 3:
                        result.httpStatus = System.Net.HttpStatusCode.Conflict;
                        result.message = "Please, introduce a different password";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
            }


            return result;
        }
        public BaseReponseModel ManagementUserLogin(ManagementUserRequest request)
        {
            BaseReponseModel result = new BaseReponseModel();
            int operationResult = 0;

            if (ManagementUserLoginValidation(request))
            {
                operationResult = managementUserDAC.ManagementUserLogin(request.managementUser);

                if (operationResult == 1) result.httpStatus = System.Net.HttpStatusCode.OK;
                else
                {
                    result.httpStatus = System.Net.HttpStatusCode.Conflict;
                    result.message = "Wrong user/password";
                }
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
            }
            return result;
        }
        private bool ManagementUserLoginValidation(ManagementUserRequest request)
        {
            if (request != null
                && request.managementUser != null
                && !string.IsNullOrWhiteSpace(request.managementUser.Login)
                && !string.IsNullOrWhiteSpace(request.managementUser.Password)
               )
            {
                return true;
            }
            else return false;
        }
    }
}

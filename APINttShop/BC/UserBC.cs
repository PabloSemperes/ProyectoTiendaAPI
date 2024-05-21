using API_nttshop.Models;
using APINttShop.DAC;
using APINttShop.Models.Request;
using APINttShop.Models.Response.UserResponse;

namespace APINttShop.BC
{
    public class UserBC
    {
        private readonly UserDAC userDAC = new UserDAC();
        public GetUserResponse getUser(int id)
        {
            GetUserResponse result = new GetUserResponse();

            if (GetUserValidation(id))
            {
                result.user = userDAC.GetUser(id);

                if (result.user != null)
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
        private bool GetUserValidation(int id)
        {
            if (id != null
                && id > 0)
            {
                return true;
            }
            else return false;
        }
        public GetAllUsersResponse getAllUsers()
        {
            GetAllUsersResponse result = new GetAllUsersResponse();

            result.usersList = userDAC.GetAllUsers();

            if (result.usersList.Count() > 0)
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
        public BaseReponseModel insertUser(UserRequest request)
        {
            BaseReponseModel result = new BaseReponseModel();

            if (InsertUserValidation(request))
            {
                sbyte correctOperation = userDAC.InsertUser(request.user);

                if (correctOperation == 1)
                {
                    result.httpStatus = System.Net.HttpStatusCode.OK;
                }
                else if (correctOperation == 0)
                {
                    result.httpStatus = System.Net.HttpStatusCode.Conflict;
                    result.message = "Insert a password with at least one uppercase, lowercase and a digit. Must be at least 10 characters";
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

        private bool InsertUserValidation(UserRequest request)
        {
            if (request != null
                && request.user != null
                && !string.IsNullOrWhiteSpace(request.user.Login)
                && !string.IsNullOrWhiteSpace(request.user.Password)
                && !string.IsNullOrWhiteSpace(request.user.Name)
                && !string.IsNullOrWhiteSpace(request.user.Surname1)
                && !string.IsNullOrWhiteSpace(request.user.Email)
               )
            {
                return true;
            }
            else return false;
        }

        public BaseReponseModel UpdateUser(UserRequest request)
        {
            BaseReponseModel result = new BaseReponseModel();

            if (UpdateUserValidation(request))
            {
                bool correctOperation = userDAC.UpdateUser(request.user);

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

        private bool UpdateUserValidation(UserRequest request)
        {
            bool result = false;
            if (request != null
                && request.user != null
                && !string.IsNullOrWhiteSpace(request.user.Login)
                && !string.IsNullOrWhiteSpace(request.user.Name)
                && !string.IsNullOrWhiteSpace(request.user.Surname1)
                && !string.IsNullOrWhiteSpace(request.user.Email)
               )
            {
                result = true;

                if(request.user.PostalCode != null) 
                {
                    if (!request.user.PostalCode.All(char.IsDigit)) result = false;
                }
                if (request.user.Phone != null)
                {
                    if (!request.user.Phone.All(char.IsDigit)) result = false;
                }
            }

            return result;
        }
        public BaseReponseModel DeleteUser(IdRequest request)
        {
            BaseReponseModel result = new BaseReponseModel();

            if (DeleteUserValidation(request))
            {
                bool correctOperation = userDAC.DeleteUser(request.id);

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
        private bool DeleteUserValidation(IdRequest request)
        {
            if (request != null
                && request.id > 0)
            {
                return true;
            }
            else return false;
        }
        public BaseReponseModel UpdateUserPassword(UserRequest request)
        {
            BaseReponseModel result = new BaseReponseModel();

            if (UpdateUserValidation(request))
            {
                byte correctOperation = userDAC.UpdateUserPassword(request.user);

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
        public BaseReponseModel UserLogin(UserRequest request)
        {
            BaseReponseModel result = new BaseReponseModel();
            int operationResult = 0;

            if (UserLoginValidation(request))
            {
                 operationResult = userDAC.UserLogin(request.user);

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
        private bool UserLoginValidation(UserRequest request)
        {
            if (request != null
                && request.user != null
                && !string.IsNullOrWhiteSpace(request.user.Login)
                && !string.IsNullOrWhiteSpace(request.user.Password)
               )
            {
                return true;
            }
            else return false;
        }
    }
}

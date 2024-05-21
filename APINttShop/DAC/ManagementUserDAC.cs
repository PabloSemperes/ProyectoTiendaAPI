using APINttShop.Models;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;
using System.Data.SqlClient;
using API_nttshop.DAC;

namespace APINttShop.DAC
{
    public class ManagementUserDAC
    {
        private readonly NttshopContext context = new NttshopContext();
        public ManagementUser GetManagementUser(int idManagementUser)
        {
            ManagementUser result = new ManagementUser();

            try
            {
                result = context.ManagementUsers.Find(idManagementUser);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }

            return result;
        }
        public List<ManagementUser> GetAllManagementUsers()
        {
            List<ManagementUser> result = new List<ManagementUser>();

            try
            {
                result = context.ManagementUsers.ToList();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }

            return result;
        }
        public bool InsertManagementUser(ManagementUser managementUser)
        {
            bool operationResult = false;
            string passwordEncrypt;
            try
            {
                if (PassCheck(managementUser.Password))
                {
                    context.ManagementUsers.Add(managementUser);
                    context.SaveChanges();
                    ManagementUser? managementUserUpdate = context.ManagementUsers.Find(managementUser.PkUser);
                    passwordEncrypt = GetMD5Hash(managementUser.Password);
                    managementUserUpdate.Password = passwordEncrypt;
                    context.SaveChanges();
                    operationResult = true;
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
            return operationResult;
        }
        public bool UpdateManagementUser(ManagementUser managementUser)
        {
            bool operationResult = false;

            try
            {
                ManagementUser? managementUserUpdate = context.ManagementUsers.Find(managementUser.PkUser);
                if (managementUserUpdate != null)
                {
                    managementUserUpdate.Login = managementUser.Login;
                    managementUserUpdate.Name = managementUser.Name;
                    managementUserUpdate.Surname1 = managementUser.Surname1;
                    if (managementUser.Surname2 != null) managementUserUpdate.Surname2 = managementUser.Surname2;
                    managementUserUpdate.Email = managementUser.Email;
                    if (managementUser.Language != null) managementUserUpdate.Language = managementUser.Language;
                    context.SaveChanges();
                    operationResult = true;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }

            return operationResult;
        }
        public bool DeleteManagementUser(int idManagementUser)
        {
            bool operationResult = false;
            try
            {
                ManagementUser? managementUserDelete = context.ManagementUsers.Find(idManagementUser);
                if (managementUserDelete != null)
                {
                    context.ManagementUsers.Remove(managementUserDelete);
                    context.SaveChanges();
                    operationResult = true;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
            return operationResult;
        }
        public byte UpdateManagementUserPassword(ManagementUser managementUser)
        {
            byte operationResult = 0;

            try
            {
                ManagementUser? managementUserUpdate = context.ManagementUsers.Find(managementUser.PkUser);
                if (managementUserUpdate != null)
                {
                    if (PassCheck(managementUser.Password))
                    {

                        if (GetMD5Hash(managementUser.Password) != managementUserUpdate.Password)
                        {
                            managementUserUpdate.Password = GetMD5Hash(managementUser.Password);
                            context.SaveChanges();
                            operationResult = 1;
                        }
                        else operationResult = 3;
                    }
                    else operationResult = 2;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }

            return operationResult;
        }
        private bool PassCheck(string pass)
        {
            bool operationResult = false;
            Regex hasCaps = new Regex(@"[A-Z]+");
            Regex hasLows = new Regex(@"[a-z]+");
            Regex hasNumb = new Regex(@"\d+");
            if (hasCaps.IsMatch(pass) && hasLows.IsMatch(pass) && hasNumb.IsMatch(pass) && pass.Length > 9) operationResult = true;

            return operationResult;
        }
        private string GetMD5Hash(string str)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
                return Convert.ToBase64String(data);
            }

        }
        public int ManagementUserLogin(ManagementUser managementUser) 
        {
            int result = 0;
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand command = new SqlCommand("sp_UserLogin", conn);

            try
            {
                conn.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Login", managementUser.Login);
                command.Parameters.AddWithValue("@Password", GetMD5Hash(managementUser.Password));
                command.Parameters.AddWithValue("@Selection", 2);
                command.Parameters.AddWithValue("@Result", 0).Direction = System.Data.ParameterDirection.Output;
                command.ExecuteNonQuery();
                result = Convert.ToInt32(command.Parameters["@Result"].Value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                command.Parameters.Clear();
                conn.Close();
            }

            return result;
        }
    }
}

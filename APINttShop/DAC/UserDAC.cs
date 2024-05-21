using API_nttshop.DAC;
using APINttShop.Models;
using APINttShop.Models.Entities;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace APINttShop.DAC
{
    public class UserDAC
    {
        private readonly NttshopContext context = new NttshopContext();
        public User GetUser(int idUser)
        {
            User result = new User();

            try
            {
                result = context.Users.Find(idUser);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }

            return result;
        }
        public List<User> GetAllUsers() 
        {
            List<User> result = new List<User>();

            try
            {
                result = context.Users.ToList();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }

            return result;
        }
        public sbyte InsertUser(User user) 
        {
            sbyte operationResult = -1;
            string passwordEncrypt;
            try
            {
                if (PassCheck(user.Password))
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                    User? userUpdate = context.Users.Find(user.PkUser);
                    passwordEncrypt = GetMD5Hash(user.Password);
                    userUpdate.Password = passwordEncrypt;
                    context.SaveChanges();
                    operationResult = 1;
                }
                else operationResult = 0;

            }
            catch (Exception ex)
            {
                
            }
            return operationResult;
        }
        public bool UpdateUser(User user) 
        {
            bool operationResult = false;

            try
            {
                User? userUpdate = context.Users.Find(user.PkUser);
                if ( userUpdate != null)
                {
                    userUpdate.Login = user.Login;
                    userUpdate.Name = user.Name;
                    userUpdate.Surname1 = user.Surname1;
                    if (user.Surname2 != null) userUpdate.Surname2 = user.Surname2;
                    if (user.Adress != null) userUpdate.Adress = user.Adress;
                    if (user.Province != null) userUpdate.Province = user.Province;
                    if (user.Town != null) userUpdate.Town = user.Town;
                    if (user.PostalCode != null) userUpdate.PostalCode = user.PostalCode;
                    if (user.Phone != null) userUpdate.Phone = user.Phone;
                    userUpdate.Email = user.Email;
                    if (user.Language != null) userUpdate.Language = user.Language;
                    if (user.Rate != null) userUpdate.Rate = user.Rate;
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
        public bool DeleteUser(int idUser)
        {
            bool operationResult = false;
            try
            {
                User? userDelete = context.Users.Find(idUser);
                if (userDelete != null) 
                {
                    context.Users.Remove(userDelete);
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
        public byte UpdateUserPassword(User user)
        {
            byte operationResult = 0;

            try
            {
                User? userUpdate = context.Users.Find(user.PkUser);
                if (userUpdate != null)
                {
                    if (PassCheck(user.Password))
                    {

                        if (GetMD5Hash(user.Password) != userUpdate.Password)
                        {
                            userUpdate.Password = GetMD5Hash(user.Password);
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
            if (hasCaps.IsMatch(pass) && hasLows.IsMatch(pass) && hasNumb.IsMatch(pass) && pass.Length>9) operationResult = true;

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
        public int UserLogin(User user) 
        {
            int result = 0;
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand command = new SqlCommand("sp_UserLogin", conn);

            try
            {
                conn.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Login", user.Login);
                command.Parameters.AddWithValue("@Password", GetMD5Hash(user.Password));
                command.Parameters.AddWithValue("@Selection", 1);
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

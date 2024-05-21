using API_nttshop.BC;
using API_nttshop.Models.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Data.Common;
using System.Data.SqlClient;

namespace API_nttshop.DAC
{
    public class LanguageDAC
    {
        public List<Language> GetAllLanguages()
        {
            List<Language> result = new List<Language>();
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT PK_LANGUAGE, DESCRIPTION, ISO FROM LANGUAGES", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Language l = new Language();

                        l.idLanguage = int.Parse(reader["PK_LANGUAGE"].ToString());
                        l.description = reader["DESCRIPTION"].ToString();
                        l.iso = reader["ISO"].ToString();

                        result.Add(l);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public Language GetLanguages(int idLanguage) 
        {
            Language result = new Language();
            SqlConnection conn = new SqlConnection( ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT PK_LANGUAGE, DESCRIPTION, ISO FROM LANGUAGES WHERE PK_LANGUAGE=@idlanguage", conn);
                command.Parameters.AddWithValue("@idlanguage", idLanguage);

                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        result.idLanguage = int.Parse(reader["PK_LANGUAGE"].ToString());
                        result.description = reader["DESCRIPTION"].ToString();
                        result.iso = reader["ISO"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public bool InsertLanguage(Language language)
        {
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());
            Language testLanguage = new Language();
            bool operationResult = false;

            try
            {
                conn.Open();

                SqlCommand command1 = new SqlCommand("SELECT DESCRIPTION FROM LANGUAGES WHERE ISO=@iso", conn);
                command1.Parameters.AddWithValue("@iso", language.iso);

                using (SqlDataReader reader = command1.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        testLanguage.description = reader["DESCRIPTION"].ToString();
                    }
                }

                if (testLanguage.description == null)
                {
                    SqlCommand command2 = new SqlCommand("INSERT INTO LANGUAGES VALUES (@description, @iso)", conn);
                    command2.Parameters.AddWithValue("@description", language.description);
                    command2.Parameters.AddWithValue("@iso", language.iso);

                    int result = command2.ExecuteNonQuery();

                    if (result > 0) operationResult = true;
                }

                return operationResult;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message, Ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public byte UpdateLanguage(Language language)
        {
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());
            Language testLanguage = new Language();
            int sqlResult;
            byte result;

            try
            {
                conn.Open();

                SqlCommand command1 = new SqlCommand("SELECT DESCRIPTION FROM LANGUAGES WHERE ISO=@iso AND PK_LANGUAGE!=@idlanguage", conn);
                command1.Parameters.AddWithValue("@iso", language.iso);
                command1.Parameters.AddWithValue("@idlanguage", language.idLanguage);

                using (SqlDataReader reader = command1.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        testLanguage.description = reader["DESCRIPTION"].ToString();
                    }
                }

                if (testLanguage.description == null)
                {
                    SqlCommand command = new SqlCommand("UPDATE LANGUAGES SET DESCRIPTION=@description, ISO=@iso WHERE PK_LANGUAGE=@idLanguage", conn);
                    command.Parameters.AddWithValue("@description", language.description);
                    command.Parameters.AddWithValue("@iso", language.iso);
                    command.Parameters.AddWithValue("@idLanguage", language.idLanguage);

                    sqlResult = command.ExecuteNonQuery();

                    if (sqlResult > 0)
                    {
                        result = 1;
                    }
                    else
                    {
                        result = 0;
                    }
                }
                else result = 2;

                return result;
            }
            catch(Exception Ex)
            {
                throw new Exception(Ex.Message, Ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public bool DeleteLanguage(int idLanguage)
        {
            bool result = false;
            Language lang = GetLanguages(idLanguage);
            string check1="", check2="", check3="";

            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command1 = new SqlCommand("SELECT Title FROM ProductDescription WHERE [Language]=@iso", conn);
                command1.Parameters.AddWithValue("@iso", lang.iso);

                using (SqlDataReader reader = command1.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        check1 = reader["Title"].ToString();
                    }
                }

                SqlCommand command2 = new SqlCommand("SELECT Login FROM Users WHERE [Language]=@iso", conn);
                command2.Parameters.AddWithValue("@iso", lang.iso);

                using (SqlDataReader reader = command2.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        check2 = reader["Login"].ToString();
                    }
                }

                SqlCommand command3 = new SqlCommand("SELECT Login FROM ManagementUsers WHERE [Language]=@iso", conn);
                command3.Parameters.AddWithValue("@iso", lang.iso);

                using (SqlDataReader reader = command3.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        check3 = reader["Login"].ToString();
                    }
                }

                if (string.IsNullOrEmpty(check1) && string.IsNullOrEmpty(check2) && string.IsNullOrEmpty(check3))
                {
                    SqlCommand command = new SqlCommand("DELETE FROM LANGUAGES WHERE PK_LANGUAGE=@idLanguage", conn);
                    command.Parameters.AddWithValue("@idLanguage", idLanguage);

                    command.ExecuteNonQuery();
                    result = true;
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message, Ex);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    }
}

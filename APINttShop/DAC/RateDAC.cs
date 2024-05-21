using API_nttshop.DAC;
using API_nttshop.Models.Entities;
using APINttShop.Models.Entities;
using System.Data.SqlClient;

namespace APINttShop.DAC
{
    public class RateDAC
    {
        public Rate GetRate(int idRate)
        {
            Rate result = new Rate();
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT PK_RATE, DESCRIPTION, [DEFAULT] FROM RATES WHERE PK_RATE=@idrate", conn);
                command.Parameters.AddWithValue("@idrate", idRate);

                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        result.idRate = int.Parse(reader["PK_RATE"].ToString());
                        result.description = reader["DESCRIPTION"].ToString();
                        result.defaultt = (bool)reader["DEFAULT"];
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

        public List<Rate> GetAllRates()
        {
            List<Rate> result = new List<Rate>();
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT PK_RATE, DESCRIPTION, [DEFAULT] FROM RATES", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Rate r = new Rate();

                        r.idRate = int.Parse(reader["PK_RATE"].ToString());
                        r.description = reader["DESCRIPTION"].ToString();
                        r.defaultt = (bool)reader["DEFAULT"];

                        result.Add(r);
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

        public bool InsertRate(Rate rate)
        {
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());
            Rate testRate = new Rate();
            bool operationResult = false;

            try
            {
                conn.Open();

                SqlCommand command2 = new SqlCommand("INSERT INTO RATES VALUES (@description, @default)", conn);
                command2.Parameters.AddWithValue("@description", rate.description);
                command2.Parameters.AddWithValue("@default", rate.defaultt);

                int result = command2.ExecuteNonQuery();

                if (result > 0) operationResult = true;

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

        public bool UpdateRate(Rate rate)
        {
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("UPDATE RATES SET [DESCRIPTION]=@description, [DEFAULT]=@default WHERE PK_RATE=@idRate", conn);
                command.Parameters.AddWithValue("@description", rate.description);
                command.Parameters.AddWithValue("@default", rate.defaultt);
                command.Parameters.AddWithValue("@idRate", rate.idRate);

                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
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
        }

        public bool DeleteRate(int idRate)
        {
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("DELETE FROM RATES WHERE PK_RATE=@idRate", conn);
                command.Parameters.AddWithValue("@idRate", idRate);

                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
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
        }
    }
}

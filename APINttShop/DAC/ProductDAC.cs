using API_nttshop.DAC;
using APINttShop.Models.Entities;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace APINttShop.DAC
{
    public class ProductDAC
    {
        public Product GetProduct(int idProduct, string? language)
        {
            Product result = new Product();
            result.descriptions = new List<ProductDescription>();
            result.rates = new List<ProductRate>();
            ProductRate temporalProductRate;
            ProductDescription temporalProductDescription;
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand command = new SqlCommand("sp_GetProduct", conn);

            try
            {
                conn.Open();

                
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idProduct",idProduct);
                if(language != null) command.Parameters.AddWithValue("@language", language);
                
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.idProduct = int.Parse(reader["PK_PRODUCT"].ToString());
                        result.stock = int.Parse(reader["Stock"].ToString());
                        result.enabled = (bool)reader["Enabled"];
                    }
                    reader.NextResult();
                    while (reader.Read())
                    {
                        temporalProductDescription = new ProductDescription();
                        temporalProductDescription.idProductDescription = int.Parse(reader["PK_productDescription"].ToString());
                        temporalProductDescription.idProduct = int.Parse(reader["FK_Product"].ToString());
                        temporalProductDescription.language = reader["Language"].ToString();
                        temporalProductDescription.title = reader["Title"].ToString();
                        temporalProductDescription.description = reader["Description"].ToString();
                        result.descriptions.Add(temporalProductDescription);
                        
                    }
                    reader.NextResult();
                    while (reader.Read()) 
                    {
                        temporalProductRate = new ProductRate();
                        temporalProductRate.idProduct = int.Parse(reader["FK_PRODUCT"].ToString());
                        temporalProductRate.idRate = int.Parse(reader["FK_RATE"].ToString());
                        temporalProductRate.price = decimal.Parse(reader["Price"].ToString());
                        result.rates.Add(temporalProductRate);
                        
                    }
                }
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
        public List<Product> GetAllProducts(string? language)
        {
            List<Product> result = new List<Product>();
            Product product;
            ProductDescription productDescription;
            ProductRate productRate;
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand command = new SqlCommand("sp_GetProductAllProducts", conn);

            try
            {
                conn.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                if(language != null) command.Parameters.AddWithValue("@language", language);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        product = new Product();
                        product.idProduct = int.Parse(reader["PK_PRODUCT"].ToString());
                        product.stock = int.Parse(reader["Stock"].ToString());
                        product.enabled = (bool)reader["Enabled"];
                        product.descriptions = new List<ProductDescription>();
                        product.rates = new List<ProductRate>();
                        result.Add(product);
                    }
                    reader.NextResult();
                    while (reader.Read())
                    {
                        productDescription = new ProductDescription();
                        productDescription.idProductDescription = int.Parse(reader["PK_productDescription"].ToString());
                        productDescription.idProduct = int.Parse(reader["FK_Product"].ToString());
                        productDescription.language = reader["Language"].ToString();
                        productDescription.title = reader["Title"].ToString();
                        productDescription.description = reader["Description"].ToString();
                        result.Find(x => x.idProduct == productDescription.idProduct).descriptions.Add(productDescription);
                    }
                    reader.NextResult();
                    while (reader.Read()) 
                    {
                        productRate = new ProductRate();
                        productRate.idProduct = int.Parse(reader["FK_PRODUCT"].ToString());
                        productRate.idRate = int.Parse(reader["FK_RATE"].ToString());
                        productRate.price = decimal.Parse(reader["Price"].ToString());
                        result.Find(x => x.idProduct == productRate.idProduct).rates.Add(productRate);
                    }
                }
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
        public bool InsertProduct(Product product) 
        {
            bool operationResult = false;
            int result;
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand command = new SqlCommand("sp_InsertProduct", conn);
            try
            {
                conn.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@stock", product.stock);
                command.Parameters.AddWithValue("@enabled", product.enabled);
                command.Parameters.AddWithValue("@language", product.descriptions[0].language);
                command.Parameters.AddWithValue("@title", product.descriptions[0].title);
                command.Parameters.AddWithValue("@description", product.descriptions[0].description);
                result = command.ExecuteNonQuery();
                operationResult = true;
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
            return operationResult;
        }
        public bool UpdatetProduct(Product product)
        {
            bool operationResult = false;
            int result;
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand command = new SqlCommand("sp_UpdateProduct", conn);
            try
            {
                conn.Open();
                foreach (ProductDescription prDesc in product.descriptions)
                {
                    command = new SqlCommand("sp_UpdateProduct", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PK_PRODUCT", product.idProduct);
                    command.Parameters.AddWithValue("@Stock", product.stock);
                    command.Parameters.AddWithValue("@Enabled", product.enabled);
                    command.Parameters.AddWithValue("@PK_ProductDescription", prDesc.idProductDescription);
                    command.Parameters.AddWithValue("@Language", prDesc.language);
                    command.Parameters.AddWithValue("@Title", prDesc.title);
                    command.Parameters.AddWithValue("@Description", prDesc.description);
                    result = command.ExecuteNonQuery();
                }
                operationResult = true;
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
            return operationResult;
        }
        public int DeleteProduct(int idProduct)
        {
            int result;
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand command = new SqlCommand("sp_DeleteProduct", conn);
            try
            {
                conn.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdProduct", idProduct);
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
        public int SetPrice(int idProduct, int idRate, decimal price)
        {
            int result;
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand command = new SqlCommand("sp_SetPrice", conn);
            try
            {
                conn.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdProduct", idProduct);
                command.Parameters.AddWithValue("@IdRate", idRate);
                command.Parameters.AddWithValue("@Price", price);
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

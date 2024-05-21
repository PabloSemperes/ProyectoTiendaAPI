using API_nttshop.DAC;
using API_nttshop.Models.Entities;
using APINttShop.BC;
using APINttShop.Models.Entities;
using System.Data.SqlClient;

namespace APINttShop.DAC
{
    public class OrderDAC
    {
        public Order GetOrder(int idOrder)
        {
            Order result = new Order();
            result.orderDetails = new List<OrderDetail>();
            OrderDetail temporalOrderDetail;
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand command = new SqlCommand("sp_GetOrder", conn);
            ProductDAC productDAC = new ProductDAC();
            UserDAC userDAC = new UserDAC();

            try
            {
                conn.Open();


                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idOrder", idOrder);
                command.Parameters.AddWithValue("@Result", 0).Direction = System.Data.ParameterDirection.Output;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.idOrder = int.Parse(reader["PK_ORDER"].ToString());
                        result.dateTime = DateTime.Parse(reader["OrderDate"].ToString());
                        result.orderStatus = int.Parse(reader["OrderStatus"].ToString());
                        result.totalPrice = decimal.Parse(reader["Total_Price"].ToString());
                        result.idUser = int.Parse(reader["FK_USER"].ToString());
                    }
                    reader.NextResult();
                    while (reader.Read())
                    {
                        temporalOrderDetail = new OrderDetail();
                        temporalOrderDetail.idOrder = int.Parse(reader["FK_ORDER"].ToString());
                        temporalOrderDetail.idProduct = int.Parse(reader["FK_PRODUCT"].ToString());
                        temporalOrderDetail.price = decimal.Parse(reader["Price"].ToString());
                        temporalOrderDetail.units = int.Parse(reader["Units"].ToString());
                        temporalOrderDetail.product = productDAC.GetProduct(temporalOrderDetail.idProduct, userDAC.GetUser(result.idUser).Language);
                        result.orderDetails.Add(temporalOrderDetail);
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
        public List<Order> GetAllOrders(DateTime? fromDate, DateTime? toDate, int? orderStatus)
        {
            List<Order> result = new List<Order>();
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand command = new SqlCommand("sp_GetAllOrders", conn);
            int addDetail = 0, numOrder;
            bool addOrder = true;
            ProductDAC productDAC = new ProductDAC();
            UserDAC userDAC = new UserDAC();

            try
            {
                conn.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                if (fromDate != null) command.Parameters.AddWithValue("@fromDate", fromDate);
                if (toDate != null) command.Parameters.AddWithValue("@toDate", toDate);
                if (orderStatus != null) command.Parameters.AddWithValue("@orderStatus", orderStatus);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Order order = new Order();
                        numOrder = int.Parse(reader["PK_ORDER"].ToString());
                        //Check for an existing order
                        for (int i = 0; i < result.Count; i++)
                        {
                            if (numOrder == result[i].idOrder)
                            {
                                addOrder = false;
                                addDetail = i;
                            }
                        }

                        order.idOrder = numOrder;
                        order.dateTime = (DateTime)reader["OrderDate"];
                        order.orderStatus = int.Parse(reader["OrderStatus"].ToString());
                        order.totalPrice = decimal.Parse(reader["Total_Price"].ToString());
                        order.idUser = int.Parse(reader["FK_USER"].ToString());
                        order.orderDetails = new List<OrderDetail>();

                        OrderDetail detail = new OrderDetail();
                        detail.idOrder = int.Parse(reader["FK_ORDER"].ToString());
                        detail.idProduct = int.Parse(reader["FK_PRODUCT"].ToString());
                        detail.price = decimal.Parse(reader["Price"].ToString());
                        detail.units = int.Parse(reader["Units"].ToString());
                        detail.product = productDAC.GetProduct(detail.idProduct, userDAC.GetUser(order.idUser).Language);
                        //If the order isn't in the list already, adds it
                        if (addOrder)
                        {
                            result.Add(order);
                            order.orderDetails.Add(detail);
                        }
                        //BUT if the order already exists, it only adds the details
                        else result[addDetail].orderDetails.Add(detail);

                        addOrder = true;
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
        public int InsertOrder(Order order)
        {
            int result = 0;
            OrderDetail temporalOrderDetail;
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand command = new SqlCommand("sp_InsertOrder", conn);

            try
            {
                conn.Open();
                //Inserting the order and the details of the first order
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OrderDate", order.dateTime);
                command.Parameters.AddWithValue("@OrderStatus", order.orderStatus);
                command.Parameters.AddWithValue("@Total_PRICE", order.totalPrice);
                command.Parameters.AddWithValue("@IdUser", order.idUser);
                command.Parameters.AddWithValue("@Result", 0).Direction = System.Data.ParameterDirection.Output;
                command.Parameters.AddWithValue("FK_PRODUCT", order.orderDetails[0].idProduct);
                command.Parameters.AddWithValue("Price", order.orderDetails[0].price);
                command.Parameters.AddWithValue("Units", order.orderDetails[0].units);
                command.ExecuteNonQuery();
                result = Convert.ToInt32(command.Parameters["@Result"].Value);

                //But if the order has more than 1 detail
                if (order.orderDetails.Count > 1)
                {
                    //Getting the Id from the order we just inserted by selecting the last order inserted
                    int idOrder = 0;
                    SqlCommand commandSelect = new SqlCommand("SELECT TOP 1 * FROM OrderDetail ORDER BY FK_ORDER DESC", conn);
                    using (SqlDataReader reader = commandSelect.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            idOrder = int.Parse(reader["FK_ORDER"].ToString());
                        }
                    }
                    //Then using that Id to insert the remaining orders
                    for (int i = 1; i < order.orderDetails.Count; i++)
                    {
                        command = new SqlCommand("sp_InsertOrderDetail", conn);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("FK_ORDER", idOrder);
                        command.Parameters.AddWithValue("FK_PRODUCT", order.orderDetails[i].idProduct);
                        command.Parameters.AddWithValue("Price", order.orderDetails[i].price);
                        command.Parameters.AddWithValue("Units", order.orderDetails[i].units);
                        command.Parameters.AddWithValue("@Result", 0).Direction = System.Data.ParameterDirection.Output;
                        command.ExecuteNonQuery();
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
        public int DeleteOrder(int idOrder)
        {
            int result = 0;
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand command = new SqlCommand("sp_DeleteOrder", conn);

            try
            {
                conn.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdOrder", idOrder);
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
        public int UpdateOrderStatus(int idOrder, int orderStatus)
        {
            int result;
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand command = new SqlCommand("sp_UpdateOrderStatus", conn);
            try
            {
                conn.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdOrder", idOrder);
                command.Parameters.AddWithValue("@OrderStatus", orderStatus);
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
        public List<OrderStatus> GetAllOrderStatus() 
        {
            List<OrderStatus> orderStatuses = new List<OrderStatus>();
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT PK_STATUS, DESCRIPTION FROM OrderStatus", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        OrderStatus order = new OrderStatus();

                        order.orderStatusId = int.Parse(reader["PK_STATUS"].ToString());
                        order.orderStatusName = reader["DESCRIPTION"].ToString();

                        orderStatuses.Add(order);
                    }
                }

            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                conn.Close();
            }

            return orderStatuses;
        }
    }
}

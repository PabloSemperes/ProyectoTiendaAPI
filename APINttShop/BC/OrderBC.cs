using API_nttshop.Models;
using APINttShop.DAC;
using APINttShop.Models.Request;
using APINttShop.Models.Response.OrderResponse;
using System.Text.RegularExpressions;

namespace APINttShop.BC
{
    public class OrderBC
    {
        private readonly OrderDAC orderDAC = new OrderDAC();

        public GetOrderResponse getOrder(IdRequest idRequest)
        {
            GetOrderResponse result = new GetOrderResponse();

            if (idRequest.id > 0)
            {
                result.order = orderDAC.GetOrder(idRequest.id);

                if (result.order != null)
                {
                    result.httpStatus = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    result.httpStatus = System.Net.HttpStatusCode.NoContent;
                    result.message = "No content";
                }
            }
            else result.httpStatus = System.Net.HttpStatusCode.BadRequest;

            return result;
        }
        public GetAllOrdersResponse getAllOrders(DateTime? fromDate, DateTime? toDate, int? orderStatus) 
        {
            GetAllOrdersResponse result = new GetAllOrdersResponse();

            //if (GetAllOrdersValidation(fromDate, toDate, orderStatus)) 
            //{
                result.orders = orderDAC.GetAllOrders(fromDate, toDate, orderStatus);

                result.httpStatus = System.Net.HttpStatusCode.OK;
            //}
            //else 
            //{
            //    result.httpStatus = System.Net.HttpStatusCode.BadRequest;
            //    result.message = "Incorrect parameters";
            //}

            return result;
        }
        private bool GetAllOrdersValidation(GetAllOrdersRequest request)
        {
            bool result = false, check1=true, check2=true;
            //Regex checkDateTime = new Regex(@"(19|20)\d{2}\/(0[1-9]|1[12])\/(0[1-9]|[12][0-9]|3[01]) (0[1-9]|1[0-9]|2[0-4]):[0-5][0-9]:[0-5][0-9]");

            if (request != null) 
            {
                //if (request.fromDate != null) check1 = checkDateTime.IsMatch(request.fromDate);
                //if (request.toDate != null) check2 = checkDateTime.IsMatch(request.toDate);
                if (check1 && check2) result = true;
            }

            return result;
        }
        public BaseReponseModel insertOrder(OrderRequest request)
        {
            BaseReponseModel result = new BaseReponseModel();

            if (InsertOrderValidation(request))
            {
                int operationResult = orderDAC.InsertOrder(request.order);

                if (operationResult == 1) result.httpStatus = System.Net.HttpStatusCode.OK;
                else
                {
                    result.httpStatus = System.Net.HttpStatusCode.Conflict;
                }
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
                result.message = "Incorrect parameters";
            }

            return result;
        }
        private bool InsertOrderValidation(OrderRequest request) 
        {
            bool result = false;
            if (request != null
                && request.order.dateTime != null
                && request.order.orderStatus >= 1
                && request.order.orderStatus <= 4
                && request.order.orderDetails != null)
            {
                result = true;
            }
            return result;
        }
        public BaseReponseModel DeleteOrder(IdRequest idRequest)
        {
            BaseReponseModel result = new BaseReponseModel();

            if (idRequest.id != null && idRequest.id > 0)
            {
                int correctOperation = orderDAC.DeleteOrder(idRequest.id);

                if (correctOperation == 1)
                {
                    result.httpStatus = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    result.httpStatus = System.Net.HttpStatusCode.NoContent;
                }
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
            }

            return result;
        }
        public BaseReponseModel UpdateOrderStatus(UpdateOrderStatusRequest request)
        {
            BaseReponseModel result = new BaseReponseModel();

            if (UpdateOrderStatusValidation(request))
            {
                int correctOperation = orderDAC.UpdateOrderStatus(request.idOrder, request.orderStatus);

                if (correctOperation == 1)
                {
                    result.httpStatus = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    result.httpStatus = System.Net.HttpStatusCode.NoContent;
                }
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
            }

            return result;
        }
        private bool UpdateOrderStatusValidation(UpdateOrderStatusRequest request)
        {
            if (request != null
                && request.idOrder > 0
                && request.orderStatus >= 1
                && request.orderStatus <= 4
               )
            {
                return true;
            }
            else return false;
        }
        public GetAllOrderStatusResponse GetAllOrderStatus() 
        {
            GetAllOrderStatusResponse result = new GetAllOrderStatusResponse();

            result.orderStatus = orderDAC.GetAllOrderStatus();

            if (result.orderStatus.Count > 0)
            {
                result.httpStatus = System.Net.HttpStatusCode.OK;
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.NoContent;
            }
            return result;
        }
    }
}

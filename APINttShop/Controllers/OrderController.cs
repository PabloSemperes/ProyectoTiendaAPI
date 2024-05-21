using API_nttshop.Models;
using APINttShop.BC;
using APINttShop.Models.Request;
using APINttShop.Models.Response.OrderResponse;
using APINttShop.Models.Response.ProductResponse;
using APINttShop.Utility.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APINttShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IHttpHandleResponse httpHandleResponse) : Controller
    {
        private readonly OrderBC orderBC = new OrderBC();

        [HttpGet]
        [Route("getOrder")]
        public ActionResult<GetOrderResponse> GetOrder(IdRequest idRequest)
        {
            GetOrderResponse result = orderBC.getOrder(idRequest);

            return httpHandleResponse.HandleResponse(result);
        }
        [HttpGet]
        [Route("getAllOrders")] ///{fromDate?}/{toDate?}/{OrderStatus?}
        public ActionResult<GetAllOrdersResponse> GetAllOrders(DateTime? fromDate=null, DateTime? toDate=null,int? orderStatus=null)
        {
            GetAllOrdersResponse result = orderBC.getAllOrders(fromDate, toDate, orderStatus);

            return httpHandleResponse.HandleResponse(result);
        }
        [HttpPost]
        [Route("insertOrder")]
        public ActionResult<BaseReponseModel> InsertOrder(OrderRequest request)
        {
            BaseReponseModel result = orderBC.insertOrder(request);

            return httpHandleResponse.HandleResponse(result);
        }
        [HttpDelete]
        [Route("deleteOrder")]
        public ActionResult<BaseReponseModel> DeleteOrder(IdRequest idRequest)
        {
            BaseReponseModel result = orderBC.DeleteOrder(idRequest);

            return httpHandleResponse.HandleResponse(result);
        }
        [HttpPut]
        [Route("updateOrderStatus")]
        public ActionResult<BaseReponseModel> UpdateOrderStatus(UpdateOrderStatusRequest request)
        {
            BaseReponseModel result = orderBC.UpdateOrderStatus(request);

            return httpHandleResponse.HandleResponse(result);
        }
        [HttpGet]
        [Route("getAllOrderStatus")]
        public ActionResult<GetAllOrderStatusResponse> GetAllOrderStatus()
        {
            GetAllOrderStatusResponse result = orderBC.GetAllOrderStatus();

            return httpHandleResponse.HandleResponse(result);
        }
    }
}

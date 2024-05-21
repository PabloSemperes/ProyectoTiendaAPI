using API_nttshop.Models;
using APINttShop.Utility.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APINttShop.Utility
{
    public class HttpHandleResponse : Controller, IHttpHandleResponse
    {
        [NonAction]
        public ActionResult HandleResponse(BaseReponseModel response)
        {
            if (response.httpStatus == System.Net.HttpStatusCode.OK)
            {
                return Ok(response);
            }
            if (response.httpStatus == System.Net.HttpStatusCode.NoContent)
            {
                return NoContent();
            }
            if (response.httpStatus == System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest(response.message);
            }
            if (response.httpStatus == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            if (response.httpStatus == System.Net.HttpStatusCode.Conflict)
            {
                return Conflict(response.message);
            }
            else
            {
                return Forbid();
            }
        }
    }
}

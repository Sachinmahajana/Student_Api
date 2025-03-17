    using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Student_Api.Model;


namespace Student_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase                                                
    {
        [NonAction]
        public ActionResult SendResponse(ApiResponse apiresponse,bool showMessage = false)
        {
            if(showMessage) {apiresponse.Message ??= Convert.ToString(Enum.Parse<StatusFlags>(Convert.ToString(apiresponse.Status)));}
            return apiresponse.Status == (byte)StatusFlags.Failed ? BadRequest(apiresponse) : Ok(apiresponse);
        }
    }
}
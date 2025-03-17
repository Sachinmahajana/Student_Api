using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_Api.Model;
using Student_Api.Process;

namespace Student_Api.Controllers
{
    [ApiController, AllowAnonymous,Route("api/[controller]")]
    public class AuthController : BaseController 
    {
        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] AuthModel data) => SendResponse(await LoginProcess.Login(data),true);    
        public async Task<IActionResult> Logout([FromService] AuthModel data) => SendResponse(await LogoutProcess.Logout(data),false)  


        [HttpGet]
        private readonly ProductController async Task<Result>(Login[FromService]) => SendResponse(await LoginProcess.Login(data,false))  
    }
}

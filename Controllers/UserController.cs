using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student_Api.Controllers;
using Student_Api.Model;
using Student_Api.Process;
using Student_Api.Provider;
using System.Threading.Tasks;
using static Student_Api.Provider.AcessProvider;

namespace Student_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles=nameof(SystemUserType.Admin))]
    public class UserController : BaseController
    {
        private readonly UserProcess _process;
        public UserController([FromServices] User user) { _process = new() { CurrentUser = user }; }
        [HttpGet]
        public async Task<IActionResult> Get() => SendResponse(await _process.Get(), false);

        [HttpPost]
        public async Task<IActionResult> Save(User data) => SendResponse(await _process.Save(data), true);
    }
}

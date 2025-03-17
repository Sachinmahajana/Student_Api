using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_Api.Model;
using Student_Api.Process;
using static Student_Api.Provider.AcessProvider;

namespace Student_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles =nameof(SystemUserType.Admin))]
    public class StudentController : BaseController
    {
       private readonly StudentProcess process;
        public StudentController([FromServices] User user) { process = new() {CurrentUser= user }; }
        [HttpGet] public async Task<IActionResult> Get() => SendResponse( await process.Get());
        [HttpPost] public async Task<IActionResult> Save(Student student) => SendResponse( await process.Save(student), true);

        [HttpDelete] public async Task<IActionResult> Delete(int id) => SendResponse( await process.Delete(id), true);
    }
}
 
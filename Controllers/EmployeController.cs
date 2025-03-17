using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_Api.Data;
using Student_Api.Model;
using Student_Api.Process;
using static Student_Api.Provider.AcessProvider;

namespace Student_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles =nameof(SystemUserType.Admin))]
    public class EmployeController : BaseController
    {
       private readonly EmployProcess process;
        
        public EmployeController( [FromServices] User user) { process = new() { CurrentUser = user }; }
        [HttpGet] public async Task<IActionResult> GetEmploy() => SendResponse(await process.GetEmploye(), false);
        [HttpPost] public async Task<IActionResult> SaveEmploy(Employe emp) => SendResponse(await process.SaveEmploye(emp), true);
        [HttpDelete] public async Task<IActionResult> DeleteEmploy(int id) => SendResponse(await process.DeleteEmploye(id), true);

        [HttpGet("filter-logs")]
        public IActionResult FilterLogs(string methodName, DateTime startDate, DateTime endDate)
        {
            try
            {
                var logs = process.FilterLogsAsnyc(methodName, startDate, endDate);
                return Ok(logs); // Returns HTTP 200 with the logs 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}"); // Returns HTTP 500 with the error message
            }
        }
    }
}

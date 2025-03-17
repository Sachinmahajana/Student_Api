using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_Api.Process;

namespace Student_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExceptionApiController : ControllerBase
    {
        private readonly ExceptionLoggingService _excets;

        public ExceptionApiController(ExceptionLoggingService excets)
        {
            _excets = excets;
        }

        [HttpGet("filter-logs")]
        public async Task<IActionResult> FilterLogs(string methodName, DateTime startDate, DateTime endDate)
        {
            try
            {
                var logs =await _excets.FilterLogsAsync(methodName, startDate, endDate);
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}

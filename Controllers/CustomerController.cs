            using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_Api.Model;
using Student_Api.Process;

namespace Student_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseController
    {
        private readonly CustomerProcess process;

        public CustomerController() { process = new CustomerProcess(); }
       [HttpGet] public async Task<IActionResult> GetCustomer() => SendResponse(await process.GetCustomer(), false);
       [HttpPost] public async Task<IActionResult> SaveCustomer(Customer custom) => SendResponse(await process.SaveCustomer(custom), true);
       [HttpDelete] public async Task<IActionResult> DeleteCustomer(int id) => SendResponse(await process.DeleteCustomer(id), true);
    }
}
                                                                            
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_Api.Data;
using Student_Api.Model;
using Student_Api.Process;

namespace Student_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpsController : BaseController
    {
        private readonly EmpLoy procces;
        public EmpsController([FromServices] User user) { procces = new() { CurrentUser=user}; }

        [HttpGet] public async Task<IActionResult> Get() => SendResponse( await procces.Get(), false);
        [HttpPost] public async Task<IActionResult> SavesEmp(Emp emps) => SendResponse(await procces.SavesEmp(emps), true);
        [HttpPost] public async Task<IActionResult> SaveProduct(Products product) => SendResponse( await proces.SaveProduct(product),true);
        [HttpDelete] public async Task<IActionResult> DeleteProduct(int id) => SendResponse(await proces.DeleteProduct(id),true);

    }
        [HttpDelete] public async Task<IActionResult> DeleteEmps(int id) => SendResponse(await procces.DeleteEmps(id), true);        
  }
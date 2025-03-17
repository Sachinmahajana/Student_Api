using System.Diagnostics;
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
    [Authorize(Roles=nameof(SystemUserType.Admin))]
    public class ProductController :BaseController
    {
        public User CurrentUser { get; set; }
        readonly ProductProcess proces;
        public ProductController([FromServices] User user) { proces = new() { CurrentUser = user }; }
        [HttpGet] public async Task<IActionResult> GetProduct() => SendResponse( await proces.GetProduct());
        [HttpPost] public async Task<IActionResult> SaveProduct(Products product) => SendResponse( await proces.SaveProduct(product),true);
        [HttpDelete] public async Task<IActionResult> DeleteProduct(int id) => SendResponse(await proces.DeleteProduct(id),true);
    }
}

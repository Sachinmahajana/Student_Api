//using System.Net;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Data.SqlClient;
//using Microsoft.Identity.Client;
//using Student_Api.Model;
//using Student_Api.Process;

//namespace Student_Api.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
// //   public class CategoryController : ControllerBase
// //   {
//        //        //   private readonly ILogger<CategoryController> _logger;
//        //        private readonly ExceptionLoggingService exceptis;
//        //        public CategoryController(ExceptionLoggingService _exceptions)
        //        {
        //            _exceptions = exceptis;
        //        }
        //        [HttpGet]
        //        public IActionResult GetCategory()
        //        {
        //            //.LogInformation("Fetching wether data...");
        //            // throw new HttpResponseException(HttpStatusCode.NotFound);
        //            try
        //            {
        //                throw new InvalidOperationException("Something went wrong");
        //                var category = "Categorys";
        //               // _logger.LogInformation("Category fetch sucessfully:{category}", category);
        //                return Ok();
        //            }
        //            catch(Exception ex)
        //            {
        //                exceptis.LogException(ex);
        //             //   exceptis.LogError(ex, "An error occured while fetching the data");
        //                return StatusCode(500, "internal server error");
        //            }
        //        }
        //       /* [httpget("{id}")]
        //        public iactionresult getcategorybyid(int id)
        //        {
        //            _logger.loginformation("fetching category data for id: {id}", id);  // correct log
        //            try
        //            {
        //                list<category> lst = new list<category>();
        //                lst.add(new category { id = 1, name = "abc" });
        //                lst.add(new category { id = 2, name = "abc2" });

        //                var cat = lst.firstordefault(x => x.id == id);

        //                if (cat == null)
        //                {
        //                    throw new exception("category not found");
        //                }

        //                _logger.loginformation("category with id {id} retrieved successfully.", id);
        //                return ok(cat);
        //            }
        //            catch (exception ex)
        //            {
        //                _logger.logerror(ex, "an error occurred while fetching category data for id: {id}", id);
        //                return statuscode(500, "internal server error occurred.");
        //            }
        //        }*/

        //        [HttpGet("{id}")]
        //        public IActionResult getcategorybyid(int id)
        //        {
        //          //  _logger.LogInformation($"fetching category data for id:{id} ");
        //            try
        //            {
        //                throw new InvalidOperationException("Something Went wrong");
        //                List<Category> lst = new List<Category>();
        //                lst.Add(new Category { Id = 1, Name = "abc" });
        //                lst.Add(new Category { Id = 2, Name = "abc2" });

        //                var cat = lst.FirstOrDefault(x => x.Id == id);

        //                if (cat == null)
        //                {
        //                    //.LogWarning("category with id {id} not found.", id);
        //                    return NotFound($"your search id {id} is not available...");
        //                }

        //               // _logger.LogInformation("category with id {id} retrieved successfully.", id);
        //                throw new Exception("test exception to trigger catch block");


        //            }
        //            catch (Exception ex)
        //            {
        //                // this block will catch all other types of exceptions
        //                //_logger.LogError(ex, "an error occurred while fetching category data for id: {id}", id);
        //                //_logger.LogError("exception type: {exceptiontype}", ex.GetType().Name);
        //                //_logger.LogError("exception message: {exceptionmessage}", ex.Message);
        //                //_logger.LogError("stack trace: {stacktrace}", ex.StackTrace);

        //                return StatusCode(500, "internal server error occurred while processing your request.");
        //            }
        //        }
        //    }
        //}
    //    private readonly ExceptionLoggingService _exceptionLoggingService;

    //    // Inject the exception logging service
    //    public CategoryController(ExceptionLoggingService exceptionLoggingService)
    //    {
    //        _exceptionLoggingService = exceptionLoggingService;
    //    }

    //    [HttpGet]
    //    public IActionResult GetCategory()
    //    {
    //        try
    //        {
    //            // Simulating an exception
    //            throw new InvalidOperationException("Something went wrong while fetching categories.");

    //            // Simulating data retrieval (will never reach this code due to the exception)
    //            var category = "Categories";
    //            return Ok();
    //        }
    //        catch (Exception ex)
    //        {
    //            // Log the exception with additional details (file path, line number, method name)
    //            _exceptionLoggingService.LogException(ex);

    //            // Return a generic 500 internal server error
    //            return StatusCode(500, "Internal server error.");
    //        }
    //    }

    //    [HttpGet("{id}")]
    //    public IActionResult GetCategoryById(int id)
    //    {
    //        try
    //        {
    //            // Simulating an exception
    //            throw new InvalidOperationException("Something went wrong while fetching category data.");

    //            // Simulating data retrieval (will never reach this code due to the exception)
    //            List<Category> lst = new List<Category>
    //            {
    //                new Category { Id = 1, Name = "abc" },
    //                new Category { Id = 2, Name = "abc2" }
    //            };

    //            var cat = lst.FirstOrDefault(x => x.Id == id);

    //            if (cat == null)
    //            {
    //                return NotFound($"Category with ID {id} is not found.");
    //            }

    //            return Ok(cat);
    //        }
    //        catch (Exception ex)
    //        {
    //            // Log the exception with additional details (file path, line number, method name)
    //            _exceptionLoggingService.LogException(ex);

    //            // Return a generic 500 internal server error
    //            return StatusCode(500, "Internal server error occurred while processing your request.");
    //        }
    //    }
    //}
//}

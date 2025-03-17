using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Student_Api.Data;
using Student_Api.Exceptions;
using Student_Api.Model;
                                        
namespace Student_Api.Process
{
    public class EmpLoy:GlobalVarialbe
    {
        //  private readonly DefaultDbContext db;
        private readonly ExceptionLoggingService _exceptionLoggingService;

        // Inject the exception logging service
        public EmpLoy()
        {
            _exceptionLoggingService = new ExceptionLoggingService();
        }

        public async Task<ApiResponse> Get()
        {
            ApiResponse response = new() { Status = (byte)StatusFlags.Success };
            using var db = new DefaultDbContext();
            response.Data = await db.emps.ToListAsync();
            response.Status = (byte)StatusFlags.Success;
            return response;
        }
        public async Task<ApiResponse> SavesEmp(Emp emps)
        {
            ApiResponse response = new() { Status = (byte)StatusFlags.Success };
            using DefaultDbContext db = new DefaultDbContext();
            if (emps.EmpId == 0 && !await db.emps.AnyAsync(s => s.EmpName == emps.EmpName)) {_=await db.emps.AddAsync(emps); }
            else if (emps.EmpId != 0 && !await db.emps.AnyAsync(s => s.EmpName == emps.EmpName && s.EmpId != emps.EmpId)) {_= db.emps.Update(emps); }
           _=await db.SaveChangesAsync();

            response.Data = await db.emps.FirstOrDefaultAsync(s => s.EmpId == emps.EmpId);
            
            return response;
        }
        public async Task<ApiResponse> DeleteEmps(int id)
        {
            ApiResponse response = new() { Status = (byte)StatusFlags.Success };
            try
            {
                using DefaultDbContext db = new DefaultDbContext();
                Emp employ = await db.emps.FirstOrDefaultAsync(s => s.EmpId == id);
                if (employ == null)
                {
                    throw new EmployeNotFoundException(id);
                }
                if (employ == await db.emps.FirstOrDefaultAsync(s => s.EmpId == id)) { db.emps.Remove(employ); db.SaveChanges(); }
                response.Data = employ;
            }           
            catch (EmployeNotFoundException ex)
            {
                response.Status = (byte)StatusFlags.Failed;
                response.Message = ex.Message;
                response.DetailedError = ex.StackTrace;
            }
            return response;
        }
        // New Method to filter exception logs
        public async Task<List<ExceptionLog>> FilterLogsAsync(string methodName, DateTime startDate, DateTime endDate)
        {
            // Call to ExceptionLoggingService's FilterLogs method
            return await _exceptionLoggingService.FilterLogsAsync(methodName, startDate, endDate);
        }
    }

}

using Microsoft.EntityFrameworkCore;
using Student_Api.Data;
using Student_Api.Model;

namespace Student_Api.Process
{
    public class EmployProcess:GlobalVarialbe
    {
        private readonly ExceptionLoggingService _exceptionLoggingService;

        // Inject the exception logging service
        public EmployProcess()
        {
            _exceptionLoggingService= new ExceptionLoggingService();    
        }

        // Method to fetch employees
        public async Task<ApiResponse> GetEmploye()
        {
            ApiResponse response = new() { Status = (byte)StatusFlags.Success };
            try
            {
                using DefaultDbContext db = new DefaultDbContext();
                if (db == null)
                {

                }
                response.Data = await db.Employees.ToListAsync();
            }
            catch (Exception ex)
            {
                _exceptionLoggingService.LogException(ex);
                response.Status = (byte)StatusFlags.Failed;
                response.Message = $"An error occurred while fetching employees: {ex.Message}";
            }
            return response;
        }

        // Method to save an employee
        public async Task<ApiResponse> SaveEmploye(Employe employ)
        {
            ApiResponse response = new() { Status = (byte)StatusFlags.Success };
            try
            {
                using DefaultDbContext db = new DefaultDbContext();
                if (employ == null)
                {
                    throw new InvalidOperationException("Outer exception during delete", new ArgumentNullException("Inner exception"));
                }
                if (employ.Employ_id == 0 && !db.Employees.Any(s => s.Employ_name == employ.Employ_name))
                {
                    db.Employees.Add(employ);
                }
                else if (employ.Employ_id != 0 && !db.Employees.Any(s => s.Employ_name == employ.Employ_name && s.Employ_id != employ.Employ_id))
                {
                    db.Employees.Update(employ);
                }
                db.SaveChanges();
                response.Data = await db.Employees.FirstOrDefaultAsync(s => s.Employ_id == employ.Employ_id);
            }
            catch (Exception ex)
            {
                _exceptionLoggingService.LogException(ex);
                response.Status = (byte)(StatusFlags.Failed);
                response.DetailedError = Convert.ToString(ex);
            }
            return response;
        }

        // Method to delete an employee
        public async Task<ApiResponse> DeleteEmploye(int id)
        {
            ApiResponse response = new() { Status = (byte)StatusFlags.Success };
            try
            {
                using DefaultDbContext db = new DefaultDbContext();
                Employe emp = await db.Employees.FirstOrDefaultAsync(s => s.Employ_id == id);
                if (emp == null)
                {
                    throw new InvalidOperationException("Outer exception during delete", new ArgumentNullException("Inner exception"));
                }
                if (emp != null)
                {
                    db.Employees.Remove(emp);
                    db.SaveChanges();
                }
                response.Data = emp;
            }
            catch (Exception ex)
            {
                _exceptionLoggingService.LogException(ex);
                response.Status = (byte)StatusFlags.Failed;
                response.DetailedError = $"Error deleting employee with ID {id}: {ex.Message}";
            }
            return response;
        }

        // New Method to filter exception logs
        public async Task<List<ExceptionLog>> FilterLogsAsnyc(string methodName, DateTime startDate, DateTime endDate)
        {
            // Call to ExceptionLoggingService's FilterLogs method
            return await _exceptionLoggingService.FilterLogsAsync(methodName, startDate, endDate);
        }
    }
}

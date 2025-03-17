using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Student_Api.Data;
using Student_Api.Exceptions;
using Student_Api.Model;

namespace Student_Api.Process
{
    public class CustomerProcess:GlobalVarialbe
    {
        public async Task<ApiResponse> GetCustomer()
        {
            ApiResponse resp = new() { Status = (byte)StatusFlags.Success };
            try
            {
                using DefaultDbContext db = new();
                resp.Data = await db.customers.ToListAsync();
            }
            catch(Exception ex)
            {
                resp.Status = (byte)StatusFlags.Failed;
                resp.Message = $"An error occured while fetching the Customers:{ex.Message}";
            }
            return resp;
        }
        public async Task<ApiResponse> SaveCustomer(Customer custom)
        {
            ApiResponse resp = new() { Status = (byte)StatusFlags.Success };
            try
            {
                using DefaultDbContext db = new();
                if (custom.Cust_Id == 0 && !await db.customers.AnyAsync(s => s.Cust_Name == custom.Cust_Name)) { _ = await db.customers.AddAsync(custom); }
                else if (custom.Cust_Id != 0 && !await db.customers.AnyAsync(s => s.Cust_Id == custom.Cust_Id)) { _ = db.customers.Update(custom); }
                await db.SaveChangesAsync();
                resp.Data = await db.customers.FirstOrDefaultAsync(s => s.Cust_Id == custom.Cust_Id);
            }
            catch (Exception ex)
            {
                resp.Status = (byte)StatusFlags.Failed;
                resp.Message = $"An error occured while saving the customer:{ex.Message}";
            }
            return resp;
        }
        public async Task<ApiResponse> DeleteCustomer(int id)
        {
            ApiResponse resp = new() { Status = (byte)StatusFlags.Success };
            try
            {
                using DefaultDbContext db = new();
                Customer customer = await db.customers.FirstOrDefaultAsync(s => s.Cust_Id==id);
                if (customer == null) { throw new EmployeNotFoundException(id); }
                else if (customer != await db.customers.FirstOrDefaultAsync(s => s.Cust_Id == id)) { db.customers.Remove(customer); await db.SaveChangesAsync(); }
                resp.Data = customer;
            }
            catch(EmployeNotFoundException ex)
            {
                resp.Status = (byte)StatusFlags.Failed;
                resp.Message = ex.Message;
                resp.DetailedError = ex.StackTrace;
            }
            return resp;
        }
    }
}
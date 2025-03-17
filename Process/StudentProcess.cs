using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Student_Api.Data;
using Student_Api.Model;

namespace Student_Api.Process;
public class StudentProcess:GlobalVarialbe
{
   // private readonly DefaultDbContext db;
    public async Task<ApiResponse> Get()
    {
        ApiResponse apiResponse = new() { Status = (byte)StatusFlags.Success };
        try
        {
            using DefaultDbContext dbContext = new DefaultDbContext();
            apiResponse.Data = await dbContext.Students.ToListAsync();
        }
        catch (Exception ex)
        {
            apiResponse.Status = (byte)(StatusFlags.Failed);
            apiResponse.Message = $"An error occured while fetching Student:{ex.Message}";
        }
        return apiResponse;
    }
    public async Task<ApiResponse> Save(Student student)
    {
        ApiResponse apiResponse = new() { Status = (byte)StatusFlags.Success };
        try
        {
            using DefaultDbContext dbContext = new DefaultDbContext();
            if (student.Stud_Id == 0 && !dbContext.Students.Any(d => d.Stud_Name == student.Stud_Name)) { dbContext.Students.Add(student); }
            else if (student.Stud_Id != 0 && dbContext.Students.Any(d => d.Stud_Id == student.Stud_Id)) { dbContext.Students.Update(student); }

            dbContext.SaveChanges();
            apiResponse.Data = await dbContext.Students.FirstOrDefaultAsync(d => d.Stud_Id == student.Stud_Id);

            //Trigger a background task to process data or send notification after saving a product
            _ = Task.Run(async () =>
            {
                try
                {
                    //Background task:perform logging or update related data asynchronously.
                     ProcessStudentSaveBackgroundTask(student);
                }
                //Handle the exceptions within the background task(e.g logging failure)
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in background task:{ex.Message}");
                }
            });
        }
        catch (Exception ex)
        {
            apiResponse.Status = (byte)(StatusFlags.Failed);
            apiResponse.Message = $"An error occured while saving Students:{ex.Message}";

        }
        return apiResponse;
    }
    //Background task to process after saving the Student
    private async Task ProcessStudentSaveBackgroundTask(Student student)
    {
        await Task.Delay(0);
        Console.WriteLine($"Background Task: Student Save With Id:{student.Stud_Id}");
    }
    //public ApiResponse SaveAddUpdate()
    //{
    //    ApiResponse resp = new() { Status = (byte)StatusFlags.Success };
    //    try
    //    {
    //        using DefaultDbContext db = new DefaultDbContext();
    //        if()
    //    }
    //}
    public async Task<ApiResponse> Delete(int id)
    {
        ApiResponse apiResponse = new() { Status = (byte)StatusFlags.Success };
        try
        {
            using DefaultDbContext dbContext = new DefaultDbContext();
            Student student = await dbContext.Students.FirstOrDefaultAsync(d => d.Stud_Id == id);
            if (student != null) { dbContext.Students.Remove(student); }
            //  else { apiResponse.Message = "Student not found"; }
            dbContext.SaveChanges();
            apiResponse.Data = student;

            _ = Task.Run(async () =>
            {
                try
                {
                    ProcessStudentDeleteBackgroundTask(student);
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Error in Background Task:{ex.Message}");
                }
            });
        }
        catch (Exception ex)
        {
            apiResponse.Status = (byte)(StatusFlags.Failed);
            apiResponse.Message = $"error occ:{ex.Message}";
        }
        return apiResponse;
    }
    private async Task ProcessStudentDeleteBackgroundTask(Student std)
    {
        await Task.Delay(0);
        Console.WriteLine($"Background Task: Student Delete With Id:{std.Stud_Id}");
    }
}

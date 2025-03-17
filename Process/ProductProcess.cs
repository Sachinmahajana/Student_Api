using Microsoft.EntityFrameworkCore;
using Student_Api.Data;
using Student_Api.Model;

namespace Student_Api.Process
{
    public class ProductProcess:GlobalVarialbe
    {
        public async Task<ApiResponse> GetProduct()
        {
            ApiResponse resp = new() { Status = (byte)StatusFlags.Success };
            try
            {
                using DefaultDbContext db = new();
                resp.Data = await db.products.ToListAsync();
              
            }
            catch (Exception ex)
            {
                resp.Status = (byte)StatusFlags.Failed;
                resp.Message = $"An error occured while fetching products:{ex.Message}";
            }   
            return resp;
        }
        public async Task<ApiResponse> SaveProduct(Products prod)
        {
            ApiResponse response = new() { Status = (byte)StatusFlags.Success };
            using DefaultDbContext db = new DefaultDbContext();
            try
            {
                if (prod.Product_Id == 0 && !await db.products.AnyAsync(d => d.Product_name == prod.Product_name))
                {
                    await db.products.AddAsync(prod);
                }
                else if (prod.Product_Id != 0 && db.products.Any(d => d.Product_Id == prod.Product_Id))
                {
                    db.products.Update(prod);
                }
                                        
                await db.SaveChangesAsync();

                response.Data = await db.products.FirstOrDefaultAsync(d => d.Product_Id == prod.Product_Id);

                // Trigger a background task to process data or send notifications after saving a product
                _ = Task.Run(async () =>
                {
                    try
                    {
                        // Background task: perform logging or update related data asynchronously
                        await ProcessProductDeletionBackgroundTask(prod);

                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions within the background task (e.g., logging failures)
                        Console.WriteLine($"Error in background task: {ex.Message}");
                    }
                });
            }
            catch (Exception ex)
            {
                response.Status = (byte)StatusFlags.Failed;
                response.Message = $"An error occurred while saving the product: {ex.Message}";
            }
            return response;
        }
        public async Task<ApiResponse> DeleteProduct(int id)
        {
            ApiResponse response = new() { Status = (byte)StatusFlags.Success };
            using DefaultDbContext db = new DefaultDbContext();
            try
            {
                Products product =  db.products.FirstOrDefault(d => d.Product_Id == id);
                if (product != null)
                {
                    db.products.Remove(product);
                    db.SaveChanges();

                    response.Data = product;

                    // Trigger background task for post-deletion operations
                     ProcessProductDeletionBackgroundTask(product);           
                }
                else
                {
                    response.Message = "Product not found";
                }
            }
            catch (Exception ex)
            {
                response.Status = (byte)StatusFlags.Failed;
                response.Message = $"An error occurred while deleting the product: {ex.Message}";
            }
            return response;
        }

     //   Background task to process after deleting the product
        private async Task ProcessProductDeletionBackgroundTask(Products product)
        {
           // Simulate a long-running task(e.g., sending a notification, logging, etc.)
            Task.Delay(0); // Simulate a delay of 3 seconds

        //    Do background work, such as logging the deletion or notifying users
            Console.WriteLine($"Background task: Product deleted with ID");
        }
    }
}


using Microsoft.EntityFrameworkCore;
using Student_Api.Data;
using Student_Api.Model;
using Student_Api.Process;
using Student_Api.Provider;

namespace Student_Api.Provider
{
    public class UserProcess:GlobalVarialbe
    {
        public async Task<ApiResponse> Get()
        {
            ApiResponse apiResponse = new() { Status = (byte)StatusFlags.Success };
            try
            {
                using DefaultDbContext defaultContext = new();
                apiResponse.Data = await defaultContext.users.AsNoTracking().ToListAsync();
            }
            catch (Exception ex) { apiResponse.Status = (byte)StatusFlags.Failed; apiResponse.DetailedError = Convert.ToString(ex); }
            return apiResponse;
        }
        public async Task<ApiResponse> Save(User user)
        {
            ApiResponse apiResponse = new() { Status = (byte)StatusFlags.Success };
            try
            {
                using DefaultDbContext defaultContext = new();

                if (user.User_id == 0 && !await defaultContext.users.AsNoTracking().AnyAsync(d => d.User_name == user.User_name)) { user.IsAdmin = false; user.Password = EncryptionProvider.Encrypt(user.Password); _ = await defaultContext.users.AddAsync(user); }
                else if (user.User_id != 0 && !await defaultContext.users.AsNoTracking().AnyAsync(d => d.User_name == user.User_name && d.User_id != user.User_id)) { _ = defaultContext.users.Update(user); }
                else { apiResponse.Status = (byte)StatusFlags.AlreadyExists; }
                _ = await defaultContext.SaveChangesAsync();
            }
            catch (Exception ex) { apiResponse.Status = (byte)StatusFlags.Failed; apiResponse.DetailedError = Convert.ToString(ex); }
            return apiResponse;
        }
        public async Task<ApiResponse> Delete(int id)
        {
            ApiResponse apiResponse = new() { Status = (byte)StatusFlags.Success };
            try
            {
                DefaultDbContext defaultContext = new();
                User data = await defaultContext.users.AsNoTracking().FirstOrDefaultAsync(d => d.User_id == id && !d.IsAdmin);
                if (data != null && CurrentUser.IsAdmin) { defaultContext.users.Remove(data); await defaultContext.SaveChangesAsync(); }
                else { apiResponse.Message = "User not found"; }
            }
            catch (Exception ex) { apiResponse.Status = (byte)StatusFlags.Failed; apiResponse.DetailedError = Convert.ToString(ex); }
            return apiResponse;
        }
    }
}

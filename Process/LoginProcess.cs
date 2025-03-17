using Microsoft.EntityFrameworkCore;
using Student_Api.Data;
using Student_Api.Model;
using Student_Api.Provider;
using System.Security.Claims;
using static Student_Api.Provider.AcessProvider;

namespace Student_Api.Process
{
    public class LoginProcess
    {
        public static async Task<ApiResponse> Login(AuthModel data)
        {
            ApiResponse apiResponse = new() { Status = (byte)StatusFlags.Failed };
            try
            {
                DefaultDbContext db = new();
                User user = await db.users.AsNoTracking().FirstOrDefaultAsync(u => u.User_name == data.User_name && u.Password == EncryptionProvider.Encrypt(data.Password));

                if (user == null) { apiResponse.Message = "Enter valid credentials"; }
                else
                {
                    user.Password = ""; DateTime expiry = DateTime.Now.Add(TimeSpan.FromHours(24) - DateTime.Now.TimeOfDay);
                    Claim additionalClaim = new(ClaimTypes.Role, user.IsAdmin ? Convert.ToString(SystemUserType.Admin) : Convert.ToString(SystemUserType.User));
                    user.AccessToken = GetUserAccessToken(user, expiry, additionalClaim);
                    apiResponse.Data = user; apiResponse.Status = (byte)StatusFlags.Success;
                }
            }
            catch (Exception ex) { apiResponse.DetailedError = Convert.ToString(ex); }
            return apiResponse;
        }
    }
}

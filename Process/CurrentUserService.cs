using Student_Api.Data;
using Student_Api.Model;

namespace Student_Api.Provider
{
    public class CurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public User GetCurrentUser()
        {
            var userClaim = _httpContextAccessor.HttpContext.User?.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userClaim))
            {
                return null; // Or throw an exception, depending on your needs
            }

            // Retrieve the User from the database or elsewhere using the UserId
            // For example, you could get it from your DbContext or another service:
            using (var dbContext = new DefaultDbContext())
            {
                return dbContext.users.FirstOrDefault(u => u.User_id.ToString() == userClaim);
            }
        }
    }
}

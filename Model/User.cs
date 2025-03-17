using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Student_Api.Model
{
    public class User
    {
        [Key]
        public int User_id { get; set; }
        [Required]
        public string User_name { get; set; }
        [Required]
        public string Password { get; set; }
        public string Email { get; set; }
        [Required] public bool IsActive { get; set; }
        [Required] public bool IsAdmin { get; set; }
        [NotMapped]  public string AccessToken { get; set; }
    }
    public class AuthModel
    {                   
        public string User_name { get; set; }                           
        public string Password { get; set; }        
    }
}

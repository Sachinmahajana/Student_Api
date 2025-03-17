using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Student_Api.Model
{
    public class Emp
    {
        [Key]
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string Qualifictaion { get; set; }
        public int Age { get; set; }
    }
}

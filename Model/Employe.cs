using System.ComponentModel.DataAnnotations;

namespace Student_Api.Model
{
    public class Employe
    {
        [Key]
        public int Employ_id { get; set; }
        public string Employ_name { get; set; }
        public string Designation { get; set; }
        public string Address { get; set; }
    }
}

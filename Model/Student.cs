using System.ComponentModel.DataAnnotations;

namespace Student_Api.Model
{
    public class Student
    {
        [Key]
        public int Stud_Id { get; set; }
        public string Stud_Name { get; set;}
        public string Stud_Designation { get; set; }
        public string Email { get; set; }
    }
}

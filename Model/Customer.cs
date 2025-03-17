using System.ComponentModel.DataAnnotations;

namespace Student_Api.Model
{
    public class Customer
    {
        [Key]
        public int Cust_Id { get; set; }
        public string Cust_Name { get; set; }

        public int Cust_Code { get; set; }
        public string Cust_Address { get; set; }

        public string Cust_Designation { get; set; }
    }
}

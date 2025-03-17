using System.ComponentModel.DataAnnotations;

namespace Student_Api.Model
{
    public class Products
    {
        [Key]
        public int Product_Id { get; set; }
        public string Product_name { get; set; }
        public decimal Product_price { get; set; }
        public int Product_quantity { get; set; }
        public string Category { get; set; }

    }
}

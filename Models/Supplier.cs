using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        [Required, MaxLength(100)]
        public string SupplierName { get; set; }
        public List<Product> Products  { get; set; }=new List<Product>();
    }
}

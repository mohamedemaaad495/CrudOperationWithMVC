using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        [Required,MaxLength(100)]
        public string ProductName { get; set; }
        [Required, MaxLength(100)]
        public string QuantityPerUnit { get; set; }
        [Required, MaxLength(100)]
        public int ReorderLevel { get; set; }
        [Required, MaxLength(100)]
        public decimal UnitPrice { get; set; }
        [Required, MaxLength(100)]
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }
        [ForeignKey("Supplier")]
        public int? SupplierID { get; set; }
        public virtual Supplier? Supplier { get; set; }
    }
}

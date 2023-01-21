using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.ViewModel
{
    public class ProductFormViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "ProductName Is Required & MaxLength is 100"), StringLength(100)]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "QuantityPerUnit Is Required & MaxLength is 100"), StringLength(100)]
        public string QuantityPerUnit { get; set; }
        //[Required(ErrorMessage = "ReorderLevel Is Required & MaxLength is 100"), StringLength(100)]
        public int ReorderLevel { get; set; }
        //[Required(ErrorMessage = "UnitPrice Is Required & MaxLength is 100"), StringLength(100)]
        public decimal UnitPrice { get; set; }
        //[Required(ErrorMessage = "UnitsInStock Is Required & MaxLength is 100"), StringLength(100)]
        public int UnitsInStock { get; set; }
        //[Required(ErrorMessage = "UnitsOnOrder Is Required & MaxLength is 100"), StringLength(100)]
        public int UnitsOnOrder { get; set; }
        [Display(Name = "Supplier")]
        public int? SupplierID { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }
    }
}

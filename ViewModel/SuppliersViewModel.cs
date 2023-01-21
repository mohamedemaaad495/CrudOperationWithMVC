using WebApplication1.Models;

namespace WebApplication1.ViewModel
{
    public class SuppliersViewModel
    {
        public string SupplierName { get; set; }
        public int SupplierId { get; set; }
        public int? ProductId { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}

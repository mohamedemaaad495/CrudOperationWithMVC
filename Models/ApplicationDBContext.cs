using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
        {
        }
        public virtual DbSet<Product>  Products { get; set; }
        public virtual DbSet<Supplier>  Suppliers { get; set; }
    }
}

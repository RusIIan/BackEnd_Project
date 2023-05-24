using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Product> Products  { get; set; }
        public DbSet<Category> Categories  { get; set; }
        public DbSet<Shipping> Shippings  { get; set; }
    }
}

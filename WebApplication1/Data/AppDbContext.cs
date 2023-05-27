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
        public DbSet<HomeProduct> HomeProducts  { get; set; }
        public DbSet<HomeCategory> HomeCategories  { get; set; }
        public DbSet<Shipping> Shippings  { get; set; }
        public DbSet<About> Abouts  { get; set; }
        public DbSet<Contact> Contacts  { get; set; }
        public DbSet<AboutPeoplePhoto> AboutPeoplePhotos { get; set; }
        public DbSet<BanerSlider> BanerSliders { get; set; }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Product> HomeProducts  { get; set; }
        public DbSet<Category> HomeCategories  { get; set; }
        public DbSet<Shipping> Shippings  { get; set; }
        public DbSet<About> Abouts  { get; set; }
        public DbSet<Contact> Contacts  { get; set; }
        public DbSet<AboutPeoplePhoto> AboutPeoplePhotos { get; set; }
        public DbSet<AdvertisingSlider> BanerSliders { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Baner> Baners { get; set; }
        public DbSet<ProductSlider> ProductSliders { get; set; }
        public DbSet<HomeDescription> HomeDescriptions { get; set; }
        public DbSet<ProductColor> Colors { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<QuickLink> QuickLinks { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<HomeHeaderInformation> HeaderInfos { get; set; }
        public DbSet<HomeHeaderPhone> HeaderPhones { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

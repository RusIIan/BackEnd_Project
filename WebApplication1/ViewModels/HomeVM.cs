using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Shipping> Shippings { get; set; }
        public List<HomeProduct> HomeProducts  { get; set; }
        public List<AdvertisingSlider> BanerSliders { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Baner> Baner { get; set; }
        public List<ProductSlider> ProductSliders { get; set; }
        public List<Testimonial> Testimonials { get; set; }
        public HomeDescription HomeDescriptions { get; set; }
        public Blog Blog { get; set; }
    }
}

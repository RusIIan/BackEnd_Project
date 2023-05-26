using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Shipping> Shippings { get; set; }
        public List<HomeProduct> HomeProducts  { get; set; }
        public List<BanerSlider> BanerSliders { get; set; }
    }
}

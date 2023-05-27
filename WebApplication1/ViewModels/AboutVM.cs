using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class AboutVM
    {
        public List<About> Abouts { get; set; }
        public About abouts { get; set; }
        public List<Shipping> Shippings { get; set; }
        public List<BanerSlider> BanerSliders { get; set; }
        public List<AboutPeoplePhoto> PeoplePhotos { get; set; }
        public AboutPeoplePhoto peoplePhotos { get; set; }
    }
}

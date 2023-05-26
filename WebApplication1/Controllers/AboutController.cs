using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;

        public AboutController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var about= await _context.Abouts.ToListAsync();
            var shipping = await _context.Shippings.ToListAsync();
            var banerSlider = await _context.BanerSliders.ToListAsync();
            var peoplePhoto = await _context.AboutPeoplePhotos.ToListAsync();
            var PeoplePhoto = await _context.AboutPeoplePhotos.FirstOrDefaultAsync();   


            var aboutVM = new AboutVM()
            {
                Abouts = about,
                Shippings = shipping,
                BanerSliders = banerSlider,
                PeoplePhotos = peoplePhoto,
                peoplePhotos = PeoplePhoto,
            };
            return View(aboutVM);
        }
    }
}

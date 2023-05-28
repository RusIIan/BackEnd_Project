using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var slider = await _context.Sliders.ToListAsync();
            var shipping = await _context.Shippings.ToListAsync();
            var homeProduct = await _context.HomeProducts.ToListAsync();
            var Baner = await _context.BanerSliders.ToListAsync();
            var blogs = await _context.Blogs.ToListAsync();
            var baner = await _context.Baners.ToListAsync();
            var ProductSlider = await _context.ProductSliders.ToListAsync();
            var homeDescription = await _context.HomeDescriptions.FirstOrDefaultAsync();
            var blog = await _context.Blogs.FirstOrDefaultAsync();


            ViewBag.Time = DateTime.Now.ToString("dd MMMM yyyy");
            HomeVM homeVM = new ()
            {
                Sliders = slider,
                Shippings = shipping,
                HomeProducts= homeProduct,
                BanerSliders = Baner,
                Blogs = blogs,
                Blog = blog,
                Baner= baner,
                ProductSliders = ProductSlider,
                HomeDescriptions = homeDescription,
            };
            return View(homeVM);
        }

 
    }
}
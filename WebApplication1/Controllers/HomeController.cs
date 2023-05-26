using Microsoft.AspNetCore.Mvc;
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
            HomeVM homeVM = new ()
            {
                Sliders = slider,
                Shippings = shipping,
                HomeProducts= homeProduct,
            };
            return View(homeVM);
        }

    }
}
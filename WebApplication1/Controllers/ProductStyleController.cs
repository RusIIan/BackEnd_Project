using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class ProductStyleController : Controller
    {
        private readonly AppDbContext _context;

        public ProductStyleController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int id)
        {
            var productStylePages = await _context.ProductStylePages.ToListAsync();
            var product = await _context.HomeProducts.Include(x=>x.Category).FirstOrDefaultAsync(x => x.Id == id);
            var ProductStylePage = await _context.ProductStylePages.FirstOrDefaultAsync();
            var products = await _context.HomeProducts.ToListAsync();
            var shippings = await _context.Shippings.ToListAsync();
            var socialMedias = await _context.SocialMedias.FirstOrDefaultAsync();
            ViewBag.Time = DateTime.Now.ToString("dd MMMM yyyy");
            ProductStyleVM productStyleVM = new()
            {
                homeProduct = product,
                productStylePage = ProductStylePage,
                productStylePages = productStylePages,
                homeProducts= products,
                Shippings = shippings,
                SocialMedias = socialMedias
            };
            return View(productStyleVM);
        }
    }
}

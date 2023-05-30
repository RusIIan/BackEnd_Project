using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
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
            var sliderProduct = await _context.ProductSliders.ToListAsync();
            var product = await _context.HomeProducts.FirstOrDefaultAsync();
            ProductStyleVM productStyleVM = new()
            {
                productSliders = sliderProduct,
                homeProduct = product
            };
            return View(productStyleVM);
        }
    }
}

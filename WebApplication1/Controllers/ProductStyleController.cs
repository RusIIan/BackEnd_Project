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

        public async Task<IActionResult> Index(object ProductStyleVm)
        {
            var sliderProduct = await _context.ProductSliders.ToListAsync();
            ProductStyleVM productStyleVM = new()
            {
                productSliders = sliderProduct,
            };
            return View(productStyleVM);
        }
    }
}

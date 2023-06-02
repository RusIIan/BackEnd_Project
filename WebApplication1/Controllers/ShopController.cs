using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;

        public ShopController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? categoryId,int? colorId)
        {
            var query = _context.HomeProducts.Include(x => x.Category).Include(x=>x.ProductColor).AsQueryable();

            if (categoryId.HasValue && categoryId.Value > 0|| colorId.HasValue && colorId.Value > 0)
            {
                query = query.Where(m => m.CategoryId == categoryId||m.ProductColorId==colorId);
            }
            var product = await query.ToListAsync();

            var category = await _context.HomeCategories.Include(x=>x.Products).ToListAsync();
            var color = await _context.Colors.Include(x=>x.Products).ToListAsync();
            var baner = await _context.Baners.FirstOrDefaultAsync();

            ShopVM shopVM = new()
            {
                Products = product,
                Baner = baner,
                Categories = category,
                Colors = color
            };
            return View(shopVM);
        }
    }
}

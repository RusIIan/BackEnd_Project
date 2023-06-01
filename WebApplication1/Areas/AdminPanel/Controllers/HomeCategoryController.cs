using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.AdminPanel.ViewModels.HomeCategory;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class HomeCategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeCategoryController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var product = await _context.HomeCategories.ToListAsync();
            return View(product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTypeVM createTypeVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Category category = new()
            {
                Type = createTypeVM.Type,
            };
            await _context.HomeCategories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.HomeProducts.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.HomeCategories.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null) return View();

            _context.HomeCategories.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var updateProduct = await _context.HomeCategories.FirstOrDefaultAsync(x => x.Id == id);
            if (updateProduct == null) return NotFound();

            var updateHomeProductVm = new UpdateTypeVM()
            {
                Type = updateProduct.Type,
            };
            return View(updateHomeProductVm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateTypeVM updateTypeVm)
        {
            var updateProduct = await _context.HomeCategories.FirstOrDefaultAsync(x => x.Id == updateTypeVm.Id);
            if (updateProduct == null) return NotFound();

            updateProduct.Type = updateTypeVm.Type;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

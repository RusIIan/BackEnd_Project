using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.AdminPanel.ViewModels.Color;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ColorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ColorController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var Color = await _context.Colors.ToListAsync();
            return View(Color);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateColorVM createColorVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            ProductColor color = new()
            {
                Name = createColorVM.color,
            };
            await _context.Colors.AddAsync(color);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var Color = await _context.Colors.FirstOrDefaultAsync(x => x.Id == id);
            if (Color == null)
                return NotFound();
            return View(Color);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var Color = await _context.Colors.FirstOrDefaultAsync(x => x.Id == id);
            if (Color == null) return View();

            _context.Colors.Remove(Color);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var updateColor = await _context.Colors.FirstOrDefaultAsync(x => x.Id == id);
            if (updateColor == null) return NotFound();

            var updateHomeColorVm = new UpdateColorVM()
            {
                color = updateColor.Name,
            };
            return View(updateHomeColorVm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateColorVM UpdateColorVM)
        {
            var updateColor = await _context.Colors.FirstOrDefaultAsync(x => x.Id == UpdateColorVM.id);
            if (updateColor == null) return NotFound();

            updateColor.Name = UpdateColorVM.color;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.AdminPanel.ViewModels.HomeDescription;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class HomeDescriptionController : Controller
    {
        private readonly AppDbContext _context;

        public HomeDescriptionController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var homeDescription = await _context.HomeDescriptions.ToListAsync();
            return View(homeDescription);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateHomeDescriptionVM createHomeDescriptionVM)
        {
            HomeDescription homeDescription = new()
            {
               ProductDescription = createHomeDescriptionVM.ProductDescription,
               BlogDescription = createHomeDescriptionVM.BlogDescription,
                TestimonialDescription = createHomeDescriptionVM.TestimonialDescription
            };
            await _context.HomeDescriptions.AddAsync(homeDescription);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var homeDescription = await _context.HomeDescriptions.FirstOrDefaultAsync(x => x.Id == id);
            if (homeDescription == null)
            {
                return NotFound();
            }
            return View(homeDescription);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var homeDescription = await _context.HomeDescriptions.FirstOrDefaultAsync(x => x.Id == id);
            if (homeDescription == null)
            {
                return NotFound();
            }
            _context.HomeDescriptions.Remove(homeDescription);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var homeDescription = _context.HomeDescriptions.FirstOrDefault(x => x.Id == id);

            var homeDescriptionView = new UpdateHomeDescriptionVM()
            {
                id = homeDescription.Id,
                ProductDescription = homeDescription.ProductDescription,
                BlogDescription = homeDescription.BlogDescription,
                TestimonialDescription = homeDescription.TestimonialDescription
            };
            return View(homeDescriptionView);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateHomeDescriptionVM updatehomeDescriptionVM)
        {
            var homeDescription = _context.HomeDescriptions.FirstOrDefault(x => x.Id == updatehomeDescriptionVM.id);

            homeDescription.ProductDescription = updatehomeDescriptionVM.ProductDescription;
            homeDescription.BlogDescription = updatehomeDescriptionVM.BlogDescription;
            homeDescription.TestimonialDescription = updatehomeDescriptionVM.TestimonialDescription;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

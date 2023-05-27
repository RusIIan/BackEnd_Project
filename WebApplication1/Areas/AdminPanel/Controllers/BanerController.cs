using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.AdminPanel.ViewModels.Baner;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class BanerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BanerController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _webHostEnvironment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var about = await _context.Baners.ToListAsync();
            return View(about);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBanerVM createBanerVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!createBanerVM.Photo.ContentType.Contains("image/"))
            {
                return View();
            }

            if (createBanerVM.Photo.Length / 1024 > 5000)
            {
                return View();
            }

            string filename = Guid.NewGuid().ToString() + "___" + createBanerVM.Photo.FileName;

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", filename);

            using FileStream stream = new(path, FileMode.Create);

            await createBanerVM.Photo.CopyToAsync(stream);

            Baner baner = new()
            {
                Label = createBanerVM.Label,
                Description = createBanerVM.Description,
                Image = filename
            };
            await _context.Baners.AddAsync(baner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var about = await _context.Baners.FirstOrDefaultAsync(a => a.Id == id);
            if (about == null)
            {
                return NotFound();
            }
            return View(about);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var baner = await _context.Baners.FirstOrDefaultAsync(a => a.Id == id);
            if (baner == null) return View();

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", baner.Image);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Baners.Remove(baner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var updateBaner = await _context.Baners.FirstOrDefaultAsync(x => x.Id == id);
            if (updateBaner == null) return NotFound();

            var updateBanerVm = new UpdateBanerVM()
            {
                Label = updateBaner.Label,
                Description = updateBaner.Description,
                Image = updateBaner.Image
            };
            return View(updateBanerVm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateBanerVM updateBanerVM)
        {
            var updateBanerUp = await _context.Baners.FirstOrDefaultAsync(x => x.Id == updateBanerVM.Id);
            if (updateBanerUp == null) return NotFound();

            if (updateBanerVM.Photo != null)
            {
                #region CreateNewImage
                if (!updateBanerVM.Photo.ContentType.Contains("image/"))
                {
                    return View();
                }

                if (updateBanerVM.Photo.Length / 1024 > 1000)
                {
                    return View();
                }

                string filename = Guid.NewGuid().ToString() + "__" + updateBanerVM.Photo.FileName;
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", filename);

                using FileStream stream = new(path, FileMode.Create);

                await updateBanerVM.Photo.CopyToAsync(stream);

                #endregion

                #region DeleteOldImage

                string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", updateBanerUp.Image);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                updateBanerUp.Image = filename;
                #endregion
            }
            updateBanerUp.Label = updateBanerVM.Label;
            updateBanerUp.Description = updateBanerVM.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

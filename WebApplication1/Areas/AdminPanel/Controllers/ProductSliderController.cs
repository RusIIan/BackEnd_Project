using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.AdminPanel.ViewModels.About;
using WebApplication1.Areas.AdminPanel.ViewModels.ProductSlider;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ProductSliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductSliderController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _webHostEnvironment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var about = await _context.ProductSliders.ToListAsync();
            return View(about);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductSliderVM createProductSliderVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!createProductSliderVM.Photo.ContentType.Contains("image/"))
            {
                return View();
            }

            if (createProductSliderVM.Photo.Length / 1024 > 5000)
            {
                return View();
            }

            string filename = Guid.NewGuid().ToString() + "___" + createProductSliderVM.Photo.FileName;

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", filename);

            using FileStream stream = new(path, FileMode.Create);

            await createProductSliderVM.Photo.CopyToAsync(stream);

            ProductSlider product = new()
            {
                Name = createProductSliderVM.Name,
                Price = createProductSliderVM.Price,
                Star = createProductSliderVM.Star,
                Image = filename,
            };
            await _context.ProductSliders.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var productSlider = await _context.ProductSliders.FirstOrDefaultAsync(a => a.Id == id);
            if (productSlider == null)
            {
                return NotFound();
            }
            return View(productSlider);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var productSlider = await _context.ProductSliders.FirstOrDefaultAsync(a => a.Id == id);
            if (productSlider == null) return View();

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", productSlider.Image);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.ProductSliders.Remove(productSlider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var productSlider = await _context.ProductSliders.FirstOrDefaultAsync(x => x.Id == id);
            if (productSlider == null) return NotFound();

            var updateProductSliderVM = new UpdateProductSliderVM()
            {
                Id = productSlider.Id,
                Name = productSlider.Name,
                Price = productSlider.Price,
                Star = productSlider.Star,
                Image = productSlider.Image,
            };
            return View(updateProductSliderVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductSliderVM updateProductSliderVM)
        {
            var updateAboutUp = await _context.ProductSliders.FirstOrDefaultAsync(x => x.Id == updateProductSliderVM.Id);
            if (updateAboutUp == null) return NotFound();

            if (updateProductSliderVM.Photo != null)
            {
                #region CreateNewImage
                if (!updateProductSliderVM.Photo.ContentType.Contains("image/"))
                {
                    return View();
                }

                if (updateProductSliderVM.Photo.Length / 1024 > 1000)
                {
                    return View();
                }

                string filename = Guid.NewGuid().ToString() + "__" + updateProductSliderVM.Photo.FileName;
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", filename);

                using FileStream stream = new(path, FileMode.Create);

                await updateProductSliderVM.Photo.CopyToAsync(stream);

                #endregion

                #region DeleteOldImage

                string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", updateAboutUp.Image);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                updateAboutUp.Image = filename;
                #endregion
            }


            updateAboutUp.Name = updateProductSliderVM.Name;
            updateAboutUp.Price = updateProductSliderVM.Price;
            updateAboutUp.Star = updateProductSliderVM.Star;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

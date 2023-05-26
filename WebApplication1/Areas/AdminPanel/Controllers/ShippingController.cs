using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.AdminPanel.ViewModels.Shipping;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ShippingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ShippingController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var shippings = await _context.Shippings.ToListAsync();
            return View(shippings);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateShippingVM createShippingVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!createShippingVM.Photo.ContentType.Contains("image/"))
            {
                return View();
            }

            if (createShippingVM.Photo.Length / 1024 > 5000)
            {
                return View();
            }
            string filename = Guid.NewGuid().ToString() + "___" + createShippingVM.Photo.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", filename);

            using FileStream stream = new(path, FileMode.Create);

            await createShippingVM.Photo.CopyToAsync(stream);

            Shipping shipping = new()
            {
                Label = createShippingVM.Label,
                Description= createShippingVM.Description,
                Image = filename
            };
            await _context.Shippings.AddAsync(shipping);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var shippings = await _context.Shippings.FirstOrDefaultAsync(x => x.Id == id);
            if (shippings == null)
                return NotFound();
            return View(shippings);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var shippingsCount = await _context.Shippings.CountAsync();
 
            var shippings = await _context.Shippings.FirstOrDefaultAsync(x => x.Id == id);

            if (shippings == null) return View();


            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", shippings.Image);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }


            _context.Shippings.Remove(shippings);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var updateshippings = await _context.Shippings.FirstOrDefaultAsync(x => x.Id == id);
            if (updateshippings == null) return NotFound();

            var updateSlideVm = new UpdateShippingVM()
            {
                Id = updateshippings.Id,
                Label = updateshippings.Label,
                Description = updateshippings.Description,
                Image = updateshippings.Image,
            };
            return View(updateSlideVm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateShippingVM updateshippingsVM)
        {
            var updateshippings = await _context.Shippings.FirstOrDefaultAsync(x => x.Id == updateshippingsVM.Id);
            if (updateshippings == null) return NotFound();

            if (updateshippingsVM.Photo !=null)
            {
                #region CreateNewImage
                if (!updateshippingsVM.Photo.ContentType.Contains("image/"))
                {
                    return View();
                }

                if (updateshippingsVM.Photo.Length / 1024 > 1000)
                {
                    return View();
                }

                string filename = Guid.NewGuid().ToString() + "__" + updateshippingsVM.Photo.FileName;
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", filename);

                using FileStream stream = new(path, FileMode.Create);

                await updateshippingsVM.Photo.CopyToAsync(stream);

                #endregion

                #region DeleteOldImage

                string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", updateshippings.Image);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                updateshippings.Image = filename;
                #endregion
            }


            updateshippings.Label = updateshippingsVM.Label;
            updateshippings.Description = updateshippingsVM.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }

}

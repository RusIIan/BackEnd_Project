using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.AdminPanel.ViewModels.ProductStypePage;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ProductStyleController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductStyleController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var ProductStylePage = await _context.ProductStylePages.ToListAsync();
            return View(ProductStylePage);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductStylePageVM createProductStylePageVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!createProductStylePageVM.Photo.ContentType.Contains("image/"))
            {
                return View();
            }

            if (createProductStylePageVM.Photo.Length / 1024 > 5000)
            {
                return View();
            }
            string filename = Guid.NewGuid().ToString() + "___" + createProductStylePageVM.Photo.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", filename);

            using FileStream stream = new(path, FileMode.Create);

            await createProductStylePageVM.Photo.CopyToAsync(stream);

            ProductStylePage productStylePage = new()
            {
                AboutreturnrequestText = createProductStylePageVM.AboutreturnrequestText,
                Guaranteetext = createProductStylePageVM.Guaranteetext,
                ClientName = createProductStylePageVM.ClientName,
                ClientComent = createProductStylePageVM.ClientComent,
                Description = createProductStylePageVM.Description,
                ShippingText= createProductStylePageVM.ShippingText,
                Image = filename
            };
            await _context.ProductStylePages.AddAsync(productStylePage);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var ProductStylePage = await _context.ProductStylePages.FirstOrDefaultAsync(x => x.Id == id);
            if (ProductStylePage == null)
                return NotFound();
            return View(ProductStylePage);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            var ProductStylePage = await _context.ProductStylePages.FirstOrDefaultAsync(x => x.Id == id);

            if (ProductStylePage == null) return View();


            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", ProductStylePage.Image);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }


            _context.ProductStylePages.Remove(ProductStylePage);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var updateProductStylePage = await _context.ProductStylePages.FirstOrDefaultAsync(x => x.Id == id);
            if (updateProductStylePage == null) return NotFound();

            var updateSlideVm = new UpdateProductStylePageVM()
            {
                id = updateProductStylePage.Id,
                AboutreturnrequestText = updateProductStylePage.AboutreturnrequestText,
                Guaranteetext = updateProductStylePage.Guaranteetext,
                ClientName = updateProductStylePage.ClientName,
                ClientComent = updateProductStylePage.ClientComent,
                Description = updateProductStylePage.Description,
                ShippingText = updateProductStylePage.ShippingText,
                Image = updateProductStylePage.Image,
            };
            return View(updateSlideVm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductStylePageVM updateProductStylePageVM)
        {
            var updateProductStylePage = await _context.ProductStylePages.FirstOrDefaultAsync(x => x.Id == updateProductStylePageVM.id);
            if (updateProductStylePage == null) return NotFound();

            if (updateProductStylePageVM.Photo != null)
            {
                #region CreateNewImage
                if (!updateProductStylePageVM.Photo.ContentType.Contains("image/"))
                {
                    return View();
                }

                if (updateProductStylePageVM.Photo.Length / 1024 > 1000)
                {
                    return View();
                }

                string filename = Guid.NewGuid().ToString() + "__" + updateProductStylePageVM.Photo.FileName;
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", filename);

                using FileStream stream = new(path, FileMode.Create);

                await updateProductStylePageVM.Photo.CopyToAsync(stream);

                #endregion

                #region DeleteOldImage

                string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", updateProductStylePage.Image);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                updateProductStylePage.Image = filename;
                #endregion
            }


            updateProductStylePageVM.AboutreturnrequestText = updateProductStylePage.AboutreturnrequestText;
            updateProductStylePageVM.Guaranteetext = updateProductStylePage.Guaranteetext;
            updateProductStylePageVM.ClientName = updateProductStylePage.ClientName;
            updateProductStylePageVM.ClientComent = updateProductStylePage.ClientComent;
            updateProductStylePageVM.Description = updateProductStylePage.Description;
            updateProductStylePageVM.ShippingText = updateProductStylePage.ShippingText;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

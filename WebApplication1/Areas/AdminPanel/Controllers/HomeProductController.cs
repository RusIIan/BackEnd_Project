﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.AdminPanel.ViewModels.HomeProduct;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class HomeProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeProductController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var product = await _context.HomeProducts.Include(x=>x.Category).ToListAsync();
            return View(product);
        }

        public async Task<IActionResult> ShowCategory(int categoryID)
        {
            ViewBag.Category = new SelectList((from cat in await _context.HomeCategories.ToListAsync()
            select new
            {
                Id = cat.Id,
                Type = cat.Type,
            }), "Id", "Type");

            var category = _context.HomeProducts.Include(x => x.Category).AsQueryable();

            if (categoryID > 0)
            {
                category = category.Where(x => x.CategoryId == categoryID);
            }
            return View(category.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateHomeProductVM createHomeProductVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!createHomeProductVM.Photo.ContentType.Contains("image/"))
            {
                return View();
            }

            if (createHomeProductVM.Photo.Length / 1024 > 1000)
            {
                return View();
            }

            string filename = Guid.NewGuid().ToString() + "___" + createHomeProductVM.Photo.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", filename);

            using FileStream stream = new(path, FileMode.Create);
            await createHomeProductVM.Photo.CopyToAsync(stream);

            ViewData["CategoryId"] = new SelectList(_context.HomeCategories, "Id", "Name", createHomeProductVM.CategoryId);
            Product product = new()
            {
                Name = createHomeProductVM.Name,
                Price = createHomeProductVM.Price,
                Star = createHomeProductVM.Star,
                CategoryId = createHomeProductVM.CategoryId,
                Image = filename
            };
            await _context.HomeProducts.AddAsync(product);
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
            var productCount = await _context.HomeProducts.CountAsync();
     
            var product = await _context.HomeProducts.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null) return View();


            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", product.Image);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            _context.HomeProducts.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var updateProduct = await _context.HomeProducts.FirstOrDefaultAsync(x => x.Id == id);
            if (updateProduct == null) return NotFound();

            var updateHomeProductVm = new UpdateHomeProductVM()
            {
                Name = updateProduct.Name,
                Price = updateProduct.Price,
                Star = updateProduct.Star,
                CategoryId = updateProduct.CategoryId,
                Image = updateProduct.Image
            };
            return View(updateHomeProductVm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateHomeProductVM updateHomeProductVm)
        {
            var updateProduct = await _context.HomeProducts.FirstOrDefaultAsync(x => x.Id == updateHomeProductVm.Id);
            if (updateProduct == null) return NotFound();

            if (updateHomeProductVm.Photo != null)
            {
                #region CreateNewImage
                if (!updateHomeProductVm.Photo.ContentType.Contains("image/"))
                {
                    return View();
                }

                if (updateHomeProductVm.Photo.Length / 1024 > 1000)
                {
                    return View();
                }

                string filename = Guid.NewGuid().ToString() + "__" + updateHomeProductVm.Photo.FileName;
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", filename);

                using FileStream stream = new(path, FileMode.Create);

                await updateHomeProductVm.Photo.CopyToAsync(stream);

                #endregion

                #region DeleteOldImage

                string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", updateProduct.Image);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                updateProduct.Image = filename;
                #endregion
            }


            updateProduct.Name = updateHomeProductVm.Name;
            updateProduct.Price = updateHomeProductVm.Price;
            updateProduct.Star = updateHomeProductVm.Star;
            updateProduct.CategoryId = updateHomeProductVm.CategoryId;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

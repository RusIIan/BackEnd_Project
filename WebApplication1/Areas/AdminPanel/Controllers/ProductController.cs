﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.AdminPanel.ViewModels.HomeProduct;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var product = await _context.HomeProducts.Include(x=>x.Category).ToListAsync();
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Category = new SelectList(await _context.HomeCategories.ToListAsync(), "Id", "Type");
            ViewBag.Color = new SelectList(await _context.Colors.ToListAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateHomeProductVM createHomeProductVM)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Category = new SelectList(await _context.HomeCategories.ToListAsync(), "Id", "Type");
                ViewBag.Color = new SelectList(await _context.Colors.ToListAsync(), "Id", "Name");
                return View(createHomeProductVM);
            }

            if (!createHomeProductVM.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "The uploaded file must be an image.");
                return View(createHomeProductVM);
            }

            if (createHomeProductVM.Photo.Length / 1024 > 1000)
            {
                ModelState.AddModelError("Photo", "The image size must not exceed 1000 KB.");
                return View(createHomeProductVM);
            }

            string filename = Guid.NewGuid().ToString() + "___" + createHomeProductVM.Photo.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", filename);

            using FileStream stream = new(path, FileMode.Create);
            await createHomeProductVM.Photo.CopyToAsync(stream);

            Product product = new()
            {
                Name = createHomeProductVM.Name,
                Price = createHomeProductVM.Price,
                Star = createHomeProductVM.Star,
                Decription = createHomeProductVM.Decription,
                CategoryId = createHomeProductVM.CategoryId,
                ProductColorId = createHomeProductVM.ProductColorId,
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
            ViewBag.Category = new SelectList(await _context.HomeCategories.ToListAsync(), "Id", "Type");
            ViewBag.Color = new SelectList(await _context.Colors.ToListAsync(), "Id", "Name");
            var updateHomeProductVm = new UpdateHomeProductVM()
            {
                Name = updateProduct.Name,
                Price = updateProduct.Price,
                Star = updateProduct.Star,
                Decription = updateProduct.Decription,
                CategoryId = updateProduct.CategoryId,
                ProductColorId = updateProduct.ProductColorId,
                Image = updateProduct.Image
            };
            return View(updateHomeProductVm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateHomeProductVM updateHomeProductVm)
        {
               ViewBag.Category = new SelectList(await _context.HomeCategories.ToListAsync(), "Id", "Type");
               ViewBag.Color = new SelectList(await _context.Colors.ToListAsync(), "Id", "Name");
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
            updateProduct.Decription = updateHomeProductVm.Decription;
            updateProduct.CategoryId = updateHomeProductVm.CategoryId;
            updateProduct.ProductColorId = updateHomeProductVm.ProductColorId;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

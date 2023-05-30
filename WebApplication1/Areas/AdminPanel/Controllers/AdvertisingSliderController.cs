﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.AdminPanel.ViewModels.AboutPeople;
using WebApplication1.Areas.AdminPanel.ViewModels.Baner;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class AdvertisingSliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdvertisingSliderController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _webHostEnvironment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var about = await _context.BanerSliders.ToListAsync();
            return View(about);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBanerSliderVm createAboutVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!createAboutVM.Photo.ContentType.Contains("image/"))
            {
                return View();
            }

            if (createAboutVM.Photo.Length / 1024 > 5000)
            {
                return View();
            }

            string filename = Guid.NewGuid().ToString() + "___" + createAboutVM.Photo.FileName;

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", filename);

            using FileStream stream = new(path, FileMode.Create);

            await createAboutVM.Photo.CopyToAsync(stream);

            AdvertisingSlider about = new()
            {
                image = filename,
            };
            await _context.BanerSliders.AddAsync(about);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var about = await _context.BanerSliders.FirstOrDefaultAsync(a => a.Id == id);
            if (about == null)
            {
                return NotFound();
            }
            return View(about);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var about = await _context.BanerSliders.FirstOrDefaultAsync(a => a.Id == id);
            if (about == null) return View();

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", about.image);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.BanerSliders.Remove(about);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var updateAbout = await _context.BanerSliders.FirstOrDefaultAsync(x => x.Id == id);
            if (updateAbout == null) return NotFound();

            var updateAboutUpVM = new UpdateBanerSliderVM()
            {
                Image = updateAbout.image
            };
            return View(updateAboutUpVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateBanerSliderVM updateAboutUpVM)
        {
            var updateAboutUp = await _context.BanerSliders.FirstOrDefaultAsync(x => x.Id == updateAboutUpVM.Id);
            if (updateAboutUp == null) return NotFound();

            if (updateAboutUpVM.Photo != null)
            {
                #region CreateNewImage
                if (!updateAboutUpVM.Photo.ContentType.Contains("image/"))
                {
                    return View();
                }

                if (updateAboutUpVM.Photo.Length / 1024 > 1000)
                {
                    return View();
                }

                string filename = Guid.NewGuid().ToString() + "__" + updateAboutUpVM.Photo.FileName;
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", filename);

                using FileStream stream = new(path, FileMode.Create);

                await updateAboutUpVM.Photo.CopyToAsync(stream);

                #endregion

                #region DeleteOldImage

                string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", updateAboutUp.image);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                updateAboutUp.image = filename;
                #endregion
            }
                    await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

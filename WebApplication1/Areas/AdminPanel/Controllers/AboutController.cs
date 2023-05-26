using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.AdminPanel.ViewModels.About;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AboutController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _webHostEnvironment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var about = await _context.Abouts.ToListAsync();
            return View(about);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAboutUpVM createAboutOurTeamVM)
        {
            if (!ModelState.IsValid)
            {
              return View();
            }
            if (!createAboutOurTeamVM.Photo.ContentType.Contains("image/"))
            {
                return View();
            }

            if (createAboutOurTeamVM.Photo.Length / 1024 > 5000)
            { 
                return View();
            }

            string filename = Guid.NewGuid().ToString() + "___" + createAboutOurTeamVM.Photo.FileName;

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", filename);

            using FileStream stream = new(path, FileMode.Create);

            await createAboutOurTeamVM.Photo.CopyToAsync(stream);

            About about = new()
            {
                Title = createAboutOurTeamVM.Title,
                VideoLink = createAboutOurTeamVM.VideoLink,
                UpDescription = createAboutOurTeamVM.Description,
                Image = filename,
            };
            await _context.Abouts.AddAsync(about);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var about = await  _context.Abouts.FirstOrDefaultAsync(a => a.Id == id);
            if (about == null)
            {
                return NotFound();
            }
            return View(about);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var about = await  _context.Abouts.FirstOrDefaultAsync(a => a.Id == id);
            if (about == null) return View();

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", about.Image);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Abouts.Remove(about);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var updateAbout = await _context.Abouts.FirstOrDefaultAsync(x => x.Id == id);
            if (updateAbout == null) return NotFound();

            var updateAboutUpVM = new UpdateAboutUpVM()
            {
               Id = updateAbout.Id,
               Title = updateAbout.Title,
               Description = updateAbout.UpDescription,
               VideoLink = updateAbout.VideoLink,
               image = updateAbout.Image,
            };
            return View(updateAboutUpVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateAboutUpVM updateAboutUpVM)
        {
            var updateAboutUp = await _context.Abouts.FirstOrDefaultAsync(x => x.Id == updateAboutUpVM.Id);
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

                string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", updateAboutUp.Image);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                updateAboutUp.Image = filename;
                #endregion
            }


            updateAboutUp.Title = updateAboutUpVM.Title;
            updateAboutUp.UpDescription = updateAboutUpVM.Description;
            updateAboutUp.VideoLink = updateAboutUpVM.VideoLink;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}

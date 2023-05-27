using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.AdminPanel.ViewModels.Blog;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public BlogController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var blog = await _context.Blogs.ToListAsync();
            return View(blog);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBlogVM createBlogVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!createBlogVM.Photo.ContentType.Contains("image/"))
            {
                return View();
            }
            if (createBlogVM.Photo.Length / 1024 > 5000)
            {
                return View();
            }

            string filename = Guid.NewGuid().ToString() + " __ " + createBlogVM.Photo.FileName;
            string path = Path.Combine(_environment.WebRootPath, "images/dbphoto", filename);

            using FileStream stream = new(path,FileMode.Create);
            await createBlogVM.Photo.CopyToAsync(stream);

            Blog blog = new()
            {
                Description = createBlogVM.Description, 
                BlogTitle = createBlogVM.BlogTitle,
                BlogDescription = createBlogVM.BlogDescription,
                Image = filename
            };
            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var blogDetails = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == id);
            if (blogDetails==null)
            {
                return NotFound();
            }
            return View(blogDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Detele(int id)
        {
            var blogDetails = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == id);
            if (blogDetails == null)
            {
                return View();
            }
            string path = Path.Combine(_environment.WebRootPath, "images/dbphot", blogDetails.Image);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Blogs.Remove(blogDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var blogDetails = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == id);
            if (blogDetails==null)
            {
                return NotFound();
            }
            var updateBlog = new UpdateBlogVM
            {
                BlogTitle = blogDetails.BlogTitle,
                BlogDescription = blogDetails.Description,
                Description = blogDetails.Description,
                Image = blogDetails.Image,
            };
            return View(updateBlog); 
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateBlogVM updateBlogVM)
        {
            var blogDetails = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == updateBlogVM.Id);
            if (blogDetails==null)  return NotFound();

            if (updateBlogVM.Photo !=null)
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                if (!updateBlogVM.Photo.ContentType.Contains("image/"))
                {
                    return View();
                }
                if (updateBlogVM.Photo.Length / 1024 >5000)
                {
                    return View();
                }
                string filename = Guid.NewGuid().ToString() + " __ " + updateBlogVM.Photo.FileName;
                string path = Path.Combine(_environment.WebRootPath, "images/dbphoto", filename);

                using FileStream stream = new(path, FileMode.Create);
                await updateBlogVM.Photo.CopyToAsync(stream);

                string oldPath = Path.Combine(_environment.WebRootPath, "images/dbphoto", blogDetails.Image);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
                blogDetails.Image = filename;
            }
            blogDetails.Description = updateBlogVM.Description;
            blogDetails.BlogTitle = updateBlogVM.BlogTitle;
            blogDetails.BlogDescription = updateBlogVM.BlogDescription;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

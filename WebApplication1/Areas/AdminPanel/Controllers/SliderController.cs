using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.AdminPanel.ViewModels.Sliders;
using WebApplication1.Data;

namespace WebApplication1.Areas.AdminPanel.Controllers;

[Area("AdminPanel")]
public class SliderController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public SliderController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var slider = await _context.Sliders.ToListAsync();
        return View(slider);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateSliderVM createSliderVM)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        if (!createSliderVM.Photo.ContentType.Contains("image/"))
        {
            return View();
        }

        if (createSliderVM.Photo.Length / 1024 > 5000)
        {
            return View();
        }
        string filename = Guid.NewGuid().ToString() + "___" + createSliderVM.Photo.FileName;
        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", filename);

        using FileStream stream = new(path, FileMode.Create);

        await createSliderVM.Photo.CopyToAsync(stream);

        Slider slider = new()
        {
            Percent = createSliderVM.Precent,
            Label = createSliderVM.Label,
            Description = createSliderVM.Description,
            Image = filename
        };
        await _context.Sliders.AddAsync(slider);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var slider = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == id);
        if (slider == null)
            return NotFound();
        return View(slider);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var sliderCount = await _context.Sliders.CountAsync();
    /*    if (sliderCount <= 2)
        {
            return RedirectToAction(nameof(Index));
        }*/
        var slider = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == id);

        if (slider == null) return View();


        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", slider.Image);

        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }


        _context.Sliders.Remove(slider);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var updateSlider = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == id);
        if (updateSlider == null) return NotFound();

        var updateSlideVm = new UpdateSliderVM()
        {
            Id = updateSlider.Id,
            Precent = updateSlider.Percent,
            Label = updateSlider.Label,
            Description = updateSlider.Description,
            Image = updateSlider.Image,
        };
        return View(updateSlideVm);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateSliderVM updateSliderVM)
    {
        var updateSlider = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == updateSliderVM.Id);
        if (updateSlider == null) return NotFound();

        if (updateSliderVM.Photo.Length > 0)
        {
            #region CreateNewImage
            if (!updateSliderVM.Photo.ContentType.Contains("image/"))
            {
                return View();
            }

            if (updateSliderVM.Photo.Length / 1024 > 1000)
            {
                return View();
            }

            string filename = Guid.NewGuid().ToString() + "__" + updateSliderVM.Photo.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto", filename);

            using FileStream stream = new(path, FileMode.Create);

            await updateSliderVM.Photo.CopyToAsync(stream);

            #endregion

            #region DeleteOldImage

            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images/dbphoto/dbphoto", updateSlider.Image);
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }

            updateSlider.Image = filename;
            #endregion
        }


        updateSlider.Label = updateSliderVM.Label;
        updateSlider.Description = updateSliderVM.Description;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}

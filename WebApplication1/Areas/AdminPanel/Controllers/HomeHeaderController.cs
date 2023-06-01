using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.AdminPanel.ViewModels.Header;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Areas.AdminPanel.Controllers;

[Area("AdminPanel")]
public class HomeHeaderController : Controller
{
    private readonly AppDbContext _context;

    public HomeHeaderController(AppDbContext context)
    {
        _context = context;
    }

    #region HomeHeaderPhone
    public async Task<IActionResult> Index()
    {
        var phone = await _context.HeaderPhones.ToListAsync();
        return View(phone);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateHomeHeaderPhoneVM createHomeHeaderPhoneVM)
    {
        HomeHeaderPhone phone = new()
        {
            PhoneNumber = createHomeHeaderPhoneVM.phoneNumber,
        };
        await _context.HeaderPhones.AddAsync(phone);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var phone = await _context.HeaderPhones.FirstOrDefaultAsync(x => x.Id == id);
        if (phone == null)
        {
            return NotFound();
        }
        return View(phone);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var phone = await _context.HeaderPhones.FirstOrDefaultAsync(x => x.Id == id);
        if (phone == null)
        {
            return NotFound();
        }
        _context.HeaderPhones.Remove(phone);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var phone = _context.HeaderPhones.FirstOrDefault(x => x.Id == id);

        var phoneView = new UpdateHomeHeaderPhoneVM()
        {
            phoneNumber = phone.PhoneNumber,
        };
        return View(phoneView);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateHomeHeaderPhoneVM updatephoneVM)
    {
        var phone = _context.HeaderPhones.FirstOrDefault(x => x.Id == updatephoneVM.Id);

        phone.PhoneNumber = updatephoneVM.phoneNumber;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    #endregion

    #region HeaderInfo
    public async Task<IActionResult> IndexView()
    {
        var HomeHeaderPhone = await _context.HeaderInfos.ToListAsync();
        return View(HomeHeaderPhone);
    }

    [HttpGet]
    public IActionResult headerInfoCreate()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> HeaderInfoCreate(CreateHeaderInfoVM createHeaderInfoVM)
    {
        HomeHeaderInformation headerInfo  = new()
        {
            Information = createHeaderInfoVM.Information,
        };
        await _context.HeaderInfos.AddAsync(headerInfo);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(IndexView));
    }


    [HttpGet]
    public async Task<IActionResult> HeaderInfoDetails(int id)
    {
        var headerInfo = await _context.HeaderInfos.FirstOrDefaultAsync(x => x.Id == id);
        if (headerInfo == null)
        {
            return NotFound();
        }
        return View(headerInfo);
    }

    [HttpPost]
    public async Task<IActionResult> HeaderInfoDelete(int id)
    {
        var headerInfo = await _context.HeaderInfos.FirstOrDefaultAsync(x => x.Id == id);
        if (headerInfo == null)
        {
            return NotFound();
        }
        _context.HeaderInfos.Remove(headerInfo);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(IndexView));
    }

    [HttpGet]
    public async Task<IActionResult> HeaderInfoUpdate(int id)
    {
        var headerInfo = _context.HeaderInfos.FirstOrDefault(x => x.Id == id);

        var headerInfoView = new UpdateHeaderInfoVM()
        {
            Information = headerInfo.Information,
        };
        return View(headerInfoView);
    }

    [HttpPost]
    public async Task<IActionResult> HeaderInfoUpdate(UpdateHeaderInfoVM updateheaderInfoVM)
    {
        var headerInfo = _context.HeaderInfos.FirstOrDefault(x => x.Id == updateheaderInfoVM.Id);

        headerInfo.Information = updateheaderInfoVM.Information;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(IndexView));
    }
    #endregion
}

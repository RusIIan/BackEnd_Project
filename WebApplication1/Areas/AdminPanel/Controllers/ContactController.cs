using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.AdminPanel.ViewModels.Contact;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Areas.AdminPanel.Controllers;

[Area("AdminPanel")]
public class ContactController : Controller
{
    private readonly AppDbContext _context;

    public ContactController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var contact = await _context.Contacts.ToListAsync();
        return View(contact);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateContactVM createContactVM)
    {
        Contact contact = new()
        {
            EmailAddress = createContactVM.Email,
            PhoneNumber = createContactVM.Phone,
            Address = createContactVM.Address,
            Description = createContactVM.Description,
        };
        await _context.Contacts.AddAsync(contact);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var contact = await _context.Contacts.FirstOrDefaultAsync(x=>x.Id==id);
        if (contact == null)
        {
            return NotFound();
        }
        return View(contact);   
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
        if (contact == null)
        {
            return NotFound();
        }
        _context.Contacts.Remove(contact);  
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var contact = _context.Contacts.FirstOrDefault(x => x.Id==id);

        var contactView = new UpdateContactVM()
        {
            Email = contact.EmailAddress,
            Phone = contact.PhoneNumber,
            Description = contact.Description,
            Address = contact.Address,
        };
        return View(contactView);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateContactVM updateContactVM)
    {
        var contact = _context.Contacts.FirstOrDefault(x => x.Id == updateContactVM.Id);

        contact.EmailAddress = updateContactVM.Email;
        contact.PhoneNumber = updateContactVM.Phone;
        contact.Description = updateContactVM.Description;
        contact.Address = updateContactVM.Address;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}

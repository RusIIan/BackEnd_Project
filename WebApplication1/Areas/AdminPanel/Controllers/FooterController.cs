using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.AdminPanel.ViewModels.Footer;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class FooterController : Controller
    {
        private readonly AppDbContext _context;

        public FooterController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        #region SocialMedia
        public async Task<IActionResult> Index()
        {
            var socialMedia = await _context.SocialMedias.ToListAsync();
            return View(socialMedia);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSocialMediaVM createSocialMediaVM)
        {
            SocialMedia social = new()
            {
                FacebookLink = createSocialMediaVM.FacebookLink,
                TwitterLink = createSocialMediaVM.TwitterLink,
                PinterestLink = createSocialMediaVM.PinterestLink,
                DribbbleLink = createSocialMediaVM.DribbbleLink
            };
            await _context.SocialMedias.AddAsync(social);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var social = await _context.SocialMedias.FirstOrDefaultAsync(x => x.Id == id);
            if (social == null)
            {
                return NotFound();
            }
            return View(social);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var social = await _context.SocialMedias.FirstOrDefaultAsync(x => x.Id == id);
            if (social == null)
            {
                return NotFound();
            }
            _context.SocialMedias.Remove(social);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var social = _context.SocialMedias.FirstOrDefault(x => x.Id == id);

            var socialView = new UpdateSocialMediaVM()
            {
                FacebookLink = social.FacebookLink,
                TwitterLink = social.TwitterLink,
                PinterestLink = social.PinterestLink,
                DribbbleLink = social.DribbbleLink,
            };
            return View(socialView);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateSocialMediaVM updatesocialVM)
        {
            var social = _context.SocialMedias.FirstOrDefault(x => x.Id == updatesocialVM.id);

            social.FacebookLink = updatesocialVM.FacebookLink;
            social.TwitterLink = updatesocialVM.TwitterLink;
            social.PinterestLink = updatesocialVM.PinterestLink;
            social.DribbbleLink = updatesocialVM.DribbbleLink;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region QuickLink
        public async Task<IActionResult> IndexView()
        {
            var socialMedia = await _context.QuickLinks.ToListAsync();
            return View(socialMedia);
        }

        [HttpGet]
        public IActionResult QuickCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> QuickCreate(CreateQuickLinkVM createQuickLinkVM)
        {
            QuickLink quick  = new()
            {
                Description = createQuickLinkVM.Description,
                PhoneNumber = createQuickLinkVM.PhoneNumber,
            };
            await _context.QuickLinks.AddAsync(quick);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexView));
        }


        [HttpGet]
        public async Task<IActionResult> QuickDetails(int id)
        {
            var social = await _context.QuickLinks.FirstOrDefaultAsync(x => x.Id == id);
            if (social == null)
            {
                return NotFound();
            }
            return View(social);
        }

        [HttpPost]
        public async Task<IActionResult> QuickDelete(int id)
        {
            var quick = await _context.QuickLinks.FirstOrDefaultAsync(x => x.Id == id);
            if (quick == null)
            {
                return NotFound();
            }
            _context.QuickLinks.Remove(quick);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexView));
        }

        [HttpGet]
        public async Task<IActionResult> QuickUpdate(int id)
        {
            var quick = _context.QuickLinks.FirstOrDefault(x => x.Id == id);

            var quickView = new UpdateQuickLinkVM()
            {
                Description = quick.Description,
                PhoneNumber = quick.PhoneNumber,
            };
            return View(quickView);
        }

        [HttpPost]
        public async Task<IActionResult> QuickUpdate(UpdateQuickLinkVM updatequickVM)
        {
            var quick = _context.QuickLinks.FirstOrDefault(x => x.Id == updatequickVM.id);

            quick.Description = updatequickVM.Description;
            quick.PhoneNumber = updatequickVM.PhoneNumber;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexView));
        }
        #endregion
    }
}

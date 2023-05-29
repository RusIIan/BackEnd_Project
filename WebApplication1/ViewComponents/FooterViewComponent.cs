using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.ViewModels;

namespace WebApplication1.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public FooterViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var quickLink = await _context.QuickLinks.FirstOrDefaultAsync();
            var socialMedia = await _context.SocialMedias.FirstOrDefaultAsync();
            FooterVM footerVM = new()
            {
                QuickLink = quickLink,
                SocialMedia = socialMedia
            };

            return await Task.FromResult(View(footerVM));
        }

    }
}

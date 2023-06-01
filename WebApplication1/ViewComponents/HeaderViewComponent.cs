using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.ViewModels;

namespace WebApplication1.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public HeaderViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var headerPhone = await _context.HeaderPhones.FirstOrDefaultAsync();
            var headerInfo = await _context.HeaderInfos.FirstOrDefaultAsync();
            HeaderVM headerVM = new()
            {
                HeaderPhone = headerPhone,
                HeaderInfo = headerInfo
            };
            return await Task.FromResult(View(headerVM));
        }
    }
}

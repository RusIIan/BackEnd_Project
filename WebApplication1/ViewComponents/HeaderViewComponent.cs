using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;

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
            //var socialMedia = awa 
            return (await Task.FromResult(View()));
        }
    }
}

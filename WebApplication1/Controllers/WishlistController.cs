using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class WishlistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

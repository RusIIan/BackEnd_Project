using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

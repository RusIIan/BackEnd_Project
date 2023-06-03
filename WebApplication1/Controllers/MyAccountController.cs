using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class MyAccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

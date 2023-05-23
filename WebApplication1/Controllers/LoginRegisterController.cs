using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class LoginRegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

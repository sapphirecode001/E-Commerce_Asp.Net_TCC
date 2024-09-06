using Microsoft.AspNetCore.Mvc;

namespace Site_SmartComfort.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}

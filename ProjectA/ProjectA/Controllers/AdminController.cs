using Microsoft.AspNetCore.Mvc;

namespace ProjectA.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Login()
        {
            return View("Login");
        }
        public IActionResult DashBoard()
        {
            return View("DashBoard");
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace ProjectA.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult SignIn()
        {
            return View("SignIn");
        }
        public IActionResult Register()
        {
            return View("Register");
        }
        public IActionResult Account()
        {
            return View("Account");
        }
    }
}

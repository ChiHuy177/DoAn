using Microsoft.AspNetCore.Mvc;

namespace ProjectA.Controllers
{
    public class NewController : Controller
    {
        public IActionResult News()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Tips()
        {
            return View("Tips");
        }

    }
}

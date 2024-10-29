using Microsoft.AspNetCore.Mvc;

namespace ProjectA.Controllers
{
    public class AboutController : Controller
    {
 
        public IActionResult Location()
        {
            return View("Location");
        }
        public IActionResult About()
        {
            return View("About");
        }
        public IActionResult Contact()
        {
            return View("Contact");
        }
        public IActionResult Gallery()
        {
            return View("Gallery");
        }
        public IActionResult Team()
        {
            return View("Team");
        }
        public IActionResult FAQ()
        {
            return View("FAQ");
        }
    }
}

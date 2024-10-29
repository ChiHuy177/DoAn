using Microsoft.AspNetCore.Mvc;
using ProjectA.Models;
using System.Diagnostics;

namespace ProjectA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View("Index");
        }
        public IActionResult Service()
        {
            return View("Service");
        }
        public IActionResult Shop()
        {
            return View("Shop");
        }

        public IActionResult ProductDetails()
        {
            return View("ProductDetails");
        }

        public IActionResult ServiceDetails()
        {
            return View("ServiceDetails");
        }
        public IActionResult Cart()
        {
            return View("Cart");
        }
        public IActionResult Account()
        {
            return View("Account");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

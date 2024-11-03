using Microsoft.AspNetCore.Mvc;
using ProjectA.Data;

namespace ProjectA.Controllers
{
    public class AdminController : Controller
    {
        private readonly MyDbContext _context;
        public AdminController(MyDbContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View("Login");
        }
        public IActionResult DashBoard()
        {
            return View("DashBoard");
        }
        public IActionResult ProductList()
        {
            var products = _context.Products.ToList();
            return View(products);
        }
        public IActionResult AddProduct()
        {
            return View("AddProduct");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ProjectA.Data;
using ProjectA.Models;
using ProjectA.Models.ViewModels;
using System.Diagnostics;

namespace ProjectA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDbContext _context;

        public HomeController(ILogger<HomeController> logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
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
            var products = _context.Products.ToList();
            return View(products);
        }

        //public IActionResult ProductDetails()
        //{
        //    return View("ProductDetails");
        //}
        public IActionResult ProductDetails(int Id)
        {
            
            var productID = _context.Products.Where(p => p.Id == Id).FirstOrDefault();
            var productCategoryId = productID.CategoryId;
            var categoryId = _context.Categories.Where(c => c.Id == productCategoryId).FirstOrDefault();
            
            if (productID == null)
            {
                return RedirectToAction("Index");
            }

            var viewModel = new ProductDetailsViewModel
            {
                Product = productID,
                Category = categoryId
            };
            return View(viewModel);
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

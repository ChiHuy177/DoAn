using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        
        public IActionResult ProductDetails(int Id)
        {
            
            var productID = _context.Products.Where(p => p.Id == Id).FirstOrDefault();
            if (productID == null)
            {
                return RedirectToAction("Index");
            }
            var productCategoryId = productID.CategoryId;
            var categoryId = _context.Categories.Where(c => c.Id == productCategoryId).FirstOrDefault();
            
            

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

        
        public IActionResult Cart(int productId, string productName, decimal price, int quantity)
        {
            var cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            CartItemViewModel cartVM = new CartItemViewModel()
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Quantity * x.Price),
            };
            return View(cartVM);
        }

        public async Task<IActionResult> AddToCart(int id, int quantity)
        {

            ProductModel product = await _context.Products.FindAsync(id);
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            CartItemModel cartItem = cart.Where(c => c.Id == id).FirstOrDefault();

            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItemModel(product, quantity)); 
            }
            HttpContext.Session.SetJson("Cart", cart);
            return Redirect(Request.Headers["Referer"].ToString());
        }
        [HttpGet]
        public IActionResult GetCartPartial() 
        { 
            var cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>(); 
            var cartViewModel = new CartItemViewModel 
            { 
                CartItems = cart, GrandTotal = cart.Sum(x => x.Quantity * x.Price) 
            }; 
            return PartialView("_CartPartial", cartViewModel); 
        }
        [HttpGet]
        public IActionResult GetSmallCartPartial()
        {
            var cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            var cartViewModel = new CartItemViewModel
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Quantity * x.Price)
            };
            return PartialView("_SmallCartPartial", cartViewModel);
        }
        [HttpPost] 
        public IActionResult UpdateCart([FromBody] List<CartItemModel> cartItems)
        {
            var cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            if (cart != null)
            {
                var newcart = new List<CartItemModel>();
                foreach (var item in cartItems)
                {
                    var oldCartItem = cart.Find(x => x.Id == item.Id);
                    if (oldCartItem != null)
                    {
                        oldCartItem.Quantity = item.Quantity;
                        newcart.Add(oldCartItem);
                    }
                }

                HttpContext.Session.SetJson("Cart", newcart);

                return Ok();
            } else 
            { 
                return BadRequest("Cart is empty.");
            }
        }

        //public IActionResult CategoryListPartial()
        //{
        //    var categories =  _context.Categories.ToList(); 
        //    var products = _context.Products.ToList();
        //    if (categories == null || products == null) 
        //    { 
        //        throw new Exception("Categories or Products list is null"); 
        //    }
        //    var viewModel = new CategoryProductViewModel { 
        //        Categories = categories, 
        //        Products = products
        //    };
        //    return PartialView("_CategoryListPartial", viewModel);
        //}
        [HttpGet]
        public IActionResult LoadCategoryMenu() 
        { 
            var categories = _context.Categories.ToList(); 
            var products = _context.Products.ToList(); 
            var viewModel = new CategoryProductViewModel 
            { 
                Categories = categories, 
                Products = products 
            }; 
            return PartialView("_CategoryListPartial", viewModel); 
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ProjectA.Data;
using ProjectA.Models;
using ProjectA.Models.ViewModels;
using System.Security.Claims;
namespace ProjectA.Controllers
{
    [Authorize(AuthenticationSchemes = "AdminScheme")]
    public class AdminController : Controller
    {
        private readonly MyDbContext _context;
        IWebHostEnvironment webHostEnvironment;
        public AdminController(MyDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;   
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View("Login");
        }




        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login (string username, string password)
        {
            var user = await _context.Clients.SingleOrDefaultAsync(u => u.Username == username && u.Password == password && u.Role == 0);
            if(user != null)

            {
                var claims = new List<Claim>
                {
                    new Claim (ClaimTypes.Name, user.Username),
                    new Claim (ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim("Admin", "true")
                };
                var claimsIdentity = new ClaimsIdentity(claims, "AdminScheme");
                await HttpContext.SignInAsync("AdminScheme", new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("DashBoard", "Admin");
            }
            ViewBag.Error = "Your username or password is incorrect";
            return View();
           
        }
        [HttpGet] 

        public async Task<IActionResult> Logout() 
        { 
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); 
            return RedirectToAction("Login", "Admin"); 
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            
            var client = new ClientModel
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Username = viewModel.Username,
                Password = viewModel.Password,
                Dob = DateTime.Now,
                Role = 0,
                Phone = viewModel.Phone,
            };
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
            return RedirectToAction("Login","Admin");
        }
        
        public IActionResult DashBoard()
        {
            return View("DashBoard");
        }
        public async Task<IActionResult> ProductListAsync()
        {   
            var products = _context.Products.ToList();
            List<ProductDetailsViewModel> list = new List<ProductDetailsViewModel>();
            foreach (var each in products)
                
            {
                var productCategoryId = each.CategoryId;
                var category = _context.Categories.Where(c => c.Id == productCategoryId).FirstOrDefault();
                var newViewModel = new ProductDetailsViewModel
                {
                    Product = each,
                    Category = category
                };
                list.Add(newViewModel);
            }
            return View(list);
        }
        [Authorize]
        public IActionResult AddProduct()
        {
            return View("AddProduct");
        }
        [Authorize]
        public async Task<IActionResult> AccountDetails()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var foundUser = await _context.Clients.FirstOrDefaultAsync(x => x.Id == int.Parse(userId));
            if (foundUser != null) 
            { 
                ViewBag.User = foundUser; 
                return View(); 
            }
            return BadRequest("User not found");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddProduct(AddProductViewModel viewModel)
        {
            Console.WriteLine(viewModel.Name);
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss_fff");
            String fileName = "";
            if (viewModel.Image != null)
            {
                String uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "uploadImages");
                fileName = viewModel.Name + "_" + currentDate + "_" + viewModel.Image.FileName;
                String filePath = Path.Combine(uploadFolder, fileName);
                viewModel.Image.CopyTo(new FileStream(filePath, FileMode.Create));

            }
            var product = new ProductModel
            {
                Name = viewModel.Name,
                ShortDescription = viewModel.ShortDescription,
                Description = viewModel.Description,
                Image = fileName,
                InStock = viewModel.InStock,
                Unit = viewModel.Unit,
                Price = viewModel.Price,
                CategoryId = viewModel.CategoryId,
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("ProductList", "Admin");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int Id)
        {
          
            var product = await _context.Products.FindAsync(Id);
            if (product is not null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("ProductList", "Admin");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);
            var categories = await _context
                .Categories
                .Where(c=> c.ParentID != -1)
                .Select(c => new CategoryModel { Id = c.Id, Name = c.Name })
                .ToListAsync();
            ViewBag.Categories = categories;
            return View(product);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditProductViewModel viewModel)
        {
            
            var product = await _context.Products.FindAsync(viewModel.Id);
            if (product is not null)
            {
                product.Name = viewModel.Name;
                product.Price = viewModel.Price;
                product.CategoryId = viewModel.CategoryId;
                product.Description = viewModel.Description;
                product.ShortDescription = viewModel.ShortDescription;
                product.InStock = viewModel.InStock;
                product.Unit = viewModel.Unit;

                await _context.SaveChangesAsync();
            }
            return RedirectToAction("ProductList", "Admin");

        }
        [Authorize]
        public IActionResult CategoryList()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddCategory(AddCategoryViewModel viewModel)
        {
            int categoryParentID = int.Parse(viewModel.ParentID);
            var category = new CategoryModel
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                ParentID = categoryParentID,
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("CategoryList", "Admin");
        }
        [HttpPost]
        [Authorize]

        public async Task<IActionResult> DeleteCategory(int Id)
        {

            var category = await _context.Categories.FindAsync(Id);
            if (category is not null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("CategoryList", "Admin");
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            return View(category);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditCategory(EditCategoryViewModel viewModel)
        {
            var category = await _context.Categories.FindAsync(viewModel.Id);
            if (category is not null)
            {
                category.Name = viewModel.Name;
                category.Description = viewModel.Description;
                category.ParentID = viewModel.ParentID;

                await _context.SaveChangesAsync();
            }
            return RedirectToAction("CategoryList", "Admin");

        }
    }
}

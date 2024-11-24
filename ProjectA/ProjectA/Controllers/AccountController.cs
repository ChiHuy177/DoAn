using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectA.Data;
using ProjectA.Models;
using ProjectA.Models.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ProjectA.Controllers
{
    public class AccountController : Controller
    {
        private readonly MyDbContext _context;
        

        public AccountController(MyDbContext context)
        {
            _context = context;
            
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View("SignIn");
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(string username, string password)
        {
            var user = await _context.Clients.SingleOrDefaultAsync(u => u.Username == username && u.Password == password && u.Role == 1);
            if (user != null)

            {
                var claims = new List<Claim>
                {
                    new Claim (ClaimTypes.Name, user.Username),
                    new Claim (ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim("User", "true")
                };
                var claimsIdentity = new ClaimsIdentity(claims, "UserScheme");
                await HttpContext.SignInAsync("UserScheme", new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Cart", "Home");
            }
            ViewBag.Error = "Your username or password is incorrect";
            return View();
        }
       
        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            Console.WriteLine(viewModel.Dob);
            var client = new ClientModel
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Username = viewModel.Username,
                Password = viewModel.Password,
                Dob = viewModel.Dob,
                Role = 1,
                Phone = viewModel.Phone,
            };
            
                await _context.Clients.AddAsync(client);
                await _context.SaveChangesAsync();          
            return RedirectToAction("SignIn", "Account");

        }
        [Authorize(AuthenticationSchemes = "UserScheme")]
        public IActionResult Account()
        {
            var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (nameIdentifier != null) {
                ClientModel found = _context.Clients.Find(int.Parse(nameIdentifier));
                if (found!=null)
                {
                    return View("Account", found);
                }
            }
            return View("Account");

        }
        [Authorize(AuthenticationSchemes = "UserScheme")]
        public IActionResult CheckOut()
        {
            return View(); 
        }

    }
}

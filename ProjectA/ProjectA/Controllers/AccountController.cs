using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectA.Data;
using ProjectA.Models;
using ProjectA.Models.ViewModels;

namespace ProjectA.Controllers
{
    public class AccountController : Controller
    {
        private readonly MyDbContext _context;
        

        public AccountController(MyDbContext context)
        {
            _context = context;
            
        }
        public IActionResult SignIn()
        {
            return View("SignIn");
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
        public IActionResult Account()
        {
            return View("Account");

        }
        
    }
}

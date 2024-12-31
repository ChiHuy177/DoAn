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
            var user = await _context.Clients.SingleOrDefaultAsync(u => u.Username == username && u.Password == password);
            if (user != null)
                if(user.Role == 1)
                {
                    var claims = new List<Claim>
                    {
                        new Claim (ClaimTypes.Name, user.Username),
                        new Claim (ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim("User", "true")
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, "UserScheme");
                    await HttpContext.SignInAsync("UserScheme", new ClaimsPrincipal(claimsIdentity));
                    HttpContext.Session.SetString("IsLoggedIn", "true");
                    return RedirectToAction("Cart", "Home");
                }
                else if (user.Role == 0)
                {
                    var claims = new List<Claim>
                {
                    new Claim (ClaimTypes.Name, user.Username),
                    new Claim (ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim("Admin", "true")
                };
                    var claimsIdentity = new ClaimsIdentity(claims, "AdminScheme");
                    await HttpContext.SignInAsync("AdminScheme", new ClaimsPrincipal(claimsIdentity));
                    HttpContext.Session.SetString("IsLoggedIn", "true");
                    HttpContext.Session.SetString("IsAdmin", "true");
                    return RedirectToAction("Cart", "Home");
                }
            ViewBag.Error = "Your username or password is incorrect";
            return View();
        }

        public async Task<IActionResult> Logout() 
        { 
            await HttpContext.SignOutAsync("UserScheme"); 
            return RedirectToAction("Index", "Home"); 
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
        [Authorize(AuthenticationSchemes = "AdminScheme")]
        [Authorize(AuthenticationSchemes = "UserScheme")]
        
        public async Task<IActionResult> Account()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId == null) { 
                return Unauthorized(); 
            }
            var client = await _context.Clients.SingleOrDefaultAsync(c => c.Id == int.Parse(userId));
            var orders = await _context.Orders.Where(o => o.ClientId == int.Parse(userId)).ToListAsync();
            var viewModel = new AllOrderViewModel 
            { 
                Client = client,
                Orders = orders
            };
            return View(viewModel);

        }
        [Authorize(AuthenticationSchemes = "UserScheme")]
        [Authorize(AuthenticationSchemes = "AdminScheme")]
        public IActionResult CheckOut()
        {
            var cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            CartItemViewModel cartVM = new CartItemViewModel()
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Quantity * x.Price),
            };
            return View(cartVM); 
        }

        [Authorize(AuthenticationSchemes = "UserScheme")]
        [HttpPost]
        public IActionResult PlaceOrder([FromBody] OrderViewModel order)
        {
            var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            if (cart.Count() > 0) {
                if (nameIdentifier != null)
                {

                    AddressModel address = new AddressModel()
                    {
                        ClientId = int.Parse(nameIdentifier),
                        Ward = order.Ward,
                        District = order.District,
                        Province = order.Province,
                        Street = order.Street,
                        AddressType = AddressType.home,
                        Default = true

                    };
                    _context.Addresses.Add(address);
                    _context.SaveChanges();
                    var addressId = address.Id;
                    OrderModel orderModel = new OrderModel()
                    {
                        ClientId = int.Parse(nameIdentifier),
                        AddressId = addressId,
                        OrderDate = DateTime.Now,
                        TotalValue = cart.Sum(x => x.Quantity * x.Price),
                        Note = order.OrderNotes,
                        PaymentMethod = order.PaymentMethod,
                    };
                    _context.Orders.Add(orderModel);
                    _context.SaveChanges();
                    var oderId = orderModel.Id;
                    foreach (var item in cart)
                    {
                        var foundProduct = _context.Products.SingleOrDefault(x => x.Id == item.Id);
                        if (foundProduct!= null && foundProduct.InStock >= item.Quantity)
                        {
                            OrderDetailsModel details = new OrderDetailsModel()
                            {
                                OrderId = oderId,
                                ProductId = item.Id,
                                Quantity = item.Quantity,
                                Price = item.Price,
                            };
                            _context.OrderDetails.Add(details);
                            _context.SaveChanges();
                            // tru lai quantity
                            foundProduct.InStock -= item.Quantity;
                            _context.SaveChanges();
                        }


                    }
                }
            }
            
           
          return Ok(new { Message = "Order placed successfully!" }); 
        }
        public async Task<IActionResult> Orders()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var orders = await _context.Orders.Where(o => o.ClientId == int.Parse(userId)).ToListAsync();
            var viewModel = new AllOrderViewModel
            {
                Orders = orders
            };
            return View(viewModel);
        }
    }

    



}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectA.Data;

namespace ProjectA.Controllers
{
    
    public class ClientController : Controller
    {
        
        private readonly MyDbContext _context;


        public ClientController(MyDbContext context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsernames()
        {
            var usernames = await _context.Clients.Select(c => c.Username).ToListAsync();
            return Json(usernames);
        }
    }
    
}

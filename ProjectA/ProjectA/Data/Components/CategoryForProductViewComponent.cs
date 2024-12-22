using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectA.Models;

namespace ProjectA.Data.Components
{
    public class CategoryForProductViewComponent : ViewComponent 
    {
        private readonly MyDbContext _dataContext;
        public CategoryForProductViewComponent(MyDbContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _dataContext
                .Categories
                .ToListAsync();
            var products = await _dataContext.Products.ToListAsync();
            ViewBag.Products = products;
            return View(categories);
        }
    }
}

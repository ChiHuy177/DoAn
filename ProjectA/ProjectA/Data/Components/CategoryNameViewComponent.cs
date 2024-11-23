using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectA.Models;

namespace ProjectA.Data.Components 
{
    public class CategoryNameViewComponent : ViewComponent
    {
        private readonly MyDbContext _dataContext;
        public CategoryNameViewComponent(MyDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _dataContext
                .Categories
                .Select(c => new CategoryModel { Id = c.Id, Name = c.Name })
                .ToListAsync();
            return View(categories);
        }

    }
}

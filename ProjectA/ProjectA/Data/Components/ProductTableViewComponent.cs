using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ProjectA.Data.Components
{
    public class ProductTableViewComponent : ViewComponent
    {
        private readonly MyDbContext _dataContext;
        public ProductTableViewComponent(MyDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IViewComponentResult> InvokeAsync() => View(await _dataContext.Products.ToListAsync());
    }
}

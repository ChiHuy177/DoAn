using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProjectA.Data.Components
{
    public class ProductsShopSecondViewComponent : ViewComponent
    {
        private readonly MyDbContext _dataContext;
        public ProductsShopSecondViewComponent(MyDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IViewComponentResult> InvokeAsync() => View(await _dataContext.Products.ToListAsync());

    }
}

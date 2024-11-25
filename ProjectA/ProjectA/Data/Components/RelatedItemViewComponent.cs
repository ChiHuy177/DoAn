
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace ProjectA.Data.Components
{
    public class RelatedItemViewComponent : ViewComponent
    {
        private readonly MyDbContext _dataContext;
        public RelatedItemViewComponent(MyDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IViewComponentResult> InvokeAsync() => View(await _dataContext.Products.ToListAsync());

        
    }
}

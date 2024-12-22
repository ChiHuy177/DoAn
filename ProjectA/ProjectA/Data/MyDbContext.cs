using Microsoft.EntityFrameworkCore;
using ProjectA.Models;

namespace ProjectA.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<ProductModel> Products { get; set; }
        public DbSet<AddressModel> Addresses { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<ClientModel> Clients { get; set; }
        public DbSet<OrderDetailsModel> OrderDetails { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
    }
}
